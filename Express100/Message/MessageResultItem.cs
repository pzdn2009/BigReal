using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Express100.Message
{
    public class MessageResultItem
    {
        [XmlElement(ElementName = "time")]
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }

        [XmlElement(ElementName = "context")]
        /// <summary>
        /// 每条数据的状态
        /// </summary>
        public string Context { get; set; }
    }
}
