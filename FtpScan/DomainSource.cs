using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.IO;
using System.Threading;
namespace FtpScan
{
    /// <summary>
    /// 获取域名列表
    /// 抽象类
    /// </summary>
    public class DomainSource
    {
        public static int ScaningThreadCount = 0;

        public DomainSource(List<string> sitedomans, int currpage1, string keywords)
        {
            this.keyword = keywords;
            this.domans = sitedomans;
            this.currpage = currpage1;
        }
        List<string> domans;

        string keyword;
        int currpage;
        public static WriteLog ThreadAddLog;

        public void GetSiteName()
        {
            try
            {
                Interlocked.Increment(ref ScaningThreadCount);
                var pageData = Common.GetPageData("http://www.baidu.com/s?wd=" + HttpUtility.UrlEncode(keyword) + "&pn=" + currpage);
                var result = Common.GetDomainListFromString(pageData);
                int cout = domans.Count;
                foreach (string index in result)
                {
                    ThreadAddLog(cout + ":" + index);
                    cout++;
                }
                //加入临时列表
                lock (domans)
                {
                    domans.AddRange(result.Where(r => !r.Contains("baidu")));
                }
            }
            catch{}
            finally
            {
                Interlocked.Decrement(ref ScaningThreadCount);
            }
        }

        /// <summary>
        /// 从文件获取域名列表
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string> LoadFormFile(string filepath)
        {

            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);


            StreamReader sr = new StreamReader(fs);
            List<string> domiannames = new List<string>();
            while (!sr.EndOfStream)
            {
                string temp = sr.ReadLine();
                if (!domiannames.Contains(temp))
                {
                    domiannames.Add(temp);
                }
            }
            sr.Close();
            fs.Close();
            fs.Dispose();
            return domiannames;
        }
        public static List<string> Dic;

        public static List<Ftp> ConvertToListFtp(List<string> domains)
        {
            List<Ftp> ftps = new List<Ftp>();
            domains.ForEach(item =>
            {
                ftps.Add(new Ftp(item, Dic));
            });
            return ftps;
        }
    }
}
