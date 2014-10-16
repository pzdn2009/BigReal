using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Express100.Message;
using Express100.Common;

namespace Express100
{
    /// <summary>
    /// 数据提取管理
    /// </summary>
    public class ExtractManager
    {
        internal DownLoader DownLoader
        {
            get { return DownLoader.NewInstance(); }
        }

        private IResultResolver resolver = null;
        private int interval = 10;

        public ExtractManager()
        {
        }

        public ExtractManager(IResultResolver resolver, int interval)
        {
            this.resolver = resolver;
            this.interval = interval;
        }

        private void EnsureResolver(ParameterBase para)
        {
            if (resolver == null || resolver.MessageType != para.Show)
            {
                resolver = FactoryResultResolver.CreateResultResolver(para.Show);
            }
        }

        public MessageResult GetMessageResult(ParameterBase para)
        {
            EnsureResolver(para);

            string str = DownLoader.GetData(para.ToUrl());
            var entity = resolver.Deserialize<MessageResult>(str);

            return entity;
        }

        public IList<MessageResult> GetMessageResults(IEnumerable<ParameterBase> paras)
        {
            var list = new List<MessageResult>();
            foreach (var pa in paras)
            {
                list.Add(GetMessageResult(pa));
                System.Threading.Thread.Sleep(interval);
            }
            return list;
        }
    }
}
