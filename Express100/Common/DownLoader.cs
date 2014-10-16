using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Express100.Common
{
    /// <summary>
    /// 下载器
    /// </summary>
    public class DownLoader
    {
        private WebClient myWebClient;
        private DownLoader()
        {
            myWebClient = new WebClient();
        }

        public static DownLoader NewInstance()
        {
            return new DownLoader();
        }

        //private static object lockObject = new object();

        //private static DownLoader instance = null;
        //public static DownLoader Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (lockObject)
        //            {
        //                instance = new DownLoader();
        //            }
        //        }
        //        return instance;
        //    }
        //}

        public string GetData(string url)
        {
            return GetData(url, Encoding.UTF8);
        }

        public string GetData(string url, Encoding encoding)
        {
            var data = myWebClient.DownloadData(url);
            return encoding.GetString(data);
        }
    }
}
