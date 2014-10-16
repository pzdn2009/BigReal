using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Express100.Message
{
    /// <summary>
    /// 快递单当前状态
    /// </summary>
    public enum EFormState : int
    {
        [XmlEnumAttribute()]
        在途 = 0,
        [XmlEnumAttribute()]
        揽件 = 1,
        [XmlEnumAttribute()]
        疑难 = 2,
        [XmlEnumAttribute()]
        签收 = 3,
        [XmlEnumAttribute()]
        退签 = 4,
        [XmlEnumAttribute()]
        派件 = 5,
        [XmlEnumAttribute()]
        退回 = 6
    }
}
