using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
namespace FtpScan
{
    public class Common
    {
        /// <summary>
        /// 查找域名正则
        /// </summary>
        static Regex FindDomain = new Regex(@"([a-z0-9]+[-]?[a-z0-9]+\.)+((com)|(net)|(org)|(gov\.cn)|(info)|(cc)|(com\.cn)|(net\.cn)|(org\.cn)|(name)|(biz)|(tv)|(cn)|(mobi)|(name)|(sh)|(ac)|(io)|(tw)|(com\.tw)|(hk)|(com\.hk)|(ws)|(travel)|(us)|(tm)|(la)|(me\.uk)|(org\.uk)|(ltd\.uk)|(plc\.uk)|(in)|(eu)|(it)|(jp))", RegexOptions.IgnoreCase);
        static Regex SingleLine = new Regex("008000[\"]>.*?</font>", RegexOptions.Compiled);

        static Regex MatchDomain = new Regex(@">(\w+[-]?\w+\.)+\w+");
        /// <summary>
        /// 在文本中查找域名
        /// 会有重复域名
        /// </summary>
        /// <param name="data">文本</param>
        /// <returns></returns>
        public static List<string> GetDomainListFromString(string data)
        {
            List<string> domains = new List<string>();

            foreach (Match item in SingleLine.Matches(data))
            {
                string temp = MatchDomain.Match(item.Value).Value.Replace(">", "");
                if (!string.IsNullOrEmpty(temp))
                {
                    domains.Add(temp.Replace(">", ""));
                }

            }
            return domains;
        }


        /// <summary>
        /// 设置请求头信息
        /// </summary>
        /// <returns></returns>
        public static string GetPageData(string url)
        {
            /// <summary>
            /// 模拟浏览器对象
            /// </summary>
            WebClient myWebClient = new WebClient();
            myWebClient.Headers.Clear(); 

            myWebClient.Headers.Add("User-agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.10) Gecko/20100914 Firefox/3.6.10");
            myWebClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            myWebClient.Headers.Add("Accept-Language", "zh-cn,zh;q=0.5");
            myWebClient.Headers.Add("Accept-Encoding", "deflate");
            myWebClient.Headers.Add("Accept-Charset", "	gb2312,utf-8;q=0.7,*;q=0.7"); 
            myWebClient.Headers.Add("Cookie", "BAIDUID=1ACBB7C422CCF64C861EF22A59B64067:FG=1");
            //下载数据
            //byte[] myDataBuffer = myWebClient.DownloadData(url);
            string temp = myWebClient.DownloadString(url);
            //string temp = Encoding.GetEncoding("gbk").GetString(myDataBuffer);
            //转换为字符串并返回
            //return Encoding.UTF8.GetString(myDataBuffer);
            return temp;
        }
        /// <summary>
        /// 删除HTML删除脚本
        /// </summary>
        /// <param name="Htmlstring"></param>
        /// <returns></returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Regex.Replace(Htmlstring, @"<script.*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<style.*?</style>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpUtility.HtmlDecode(Htmlstring).Trim();
            return Htmlstring;
        }
    }
}
