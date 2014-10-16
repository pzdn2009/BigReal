using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Express100.Message
{
    [XmlRoot(ElementName="xml")]
    [Serializable()]
    public class MessageResult
    {
        [XmlElement(ElementName = "message")]
        /// <summary>
        /// 结果消息
        /// </summary>
        public string Message { get; set; }

        [XmlElement(ElementName = "state")]
        /// <summary>
        /// 快递单状态
        /// </summary>
        public EFormState State { get; set; }

        [XmlElement(ElementName = "status")]
        /// <summary>
        /// 查询结果状态
        /// </summary>
        public EQueryResultStatus Status { get; set; }

        [XmlElement(ElementName = "nu")]
        /// <summary>
        /// 快递单号
        /// </summary>
        public string Nu { get; set; }

        [XmlElement(ElementName = "com")]
        /// <summary>
        /// 快递公司代码
        /// </summary>
        public string Com { get; set; }

        [XmlElement(ElementName = "data")]
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<MessageResultItem> Data { get; set; }
    }
}
