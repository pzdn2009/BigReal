using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogisticsTracking.Lib.Service
{
    /// <summary>
    /// 服务配置元素
    /// </summary>
    public class ServiceConfigElement
    {
        public int Interval { get; set; }
        public bool DoWorkAtStart { get; set; }
        public bool RunOnlyOnce { get; set; }
        public string Name { get; set; }

        public string Type { get; set; }
    }
}
