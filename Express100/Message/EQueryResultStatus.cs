using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Express100.Message
{
    public enum EQueryResultStatus : int
    {
        [XmlEnumAttribute()]
        运单暂无结果 = 0,
        [XmlEnumAttribute()]
        查询成功 = 1,
        [XmlEnumAttribute()]
        接口出现异常 = 2,
        [XmlEnumAttribute()]
        验证码出错 = 408
    }
}
