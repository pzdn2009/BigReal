using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Express100
{
    public class FactoryResultResolver
    {
        public static IResultResolver CreateResultResolver(EMessageType messageType)
        {
            IResultResolver ret = null;
            switch (messageType)
            {
                case EMessageType.Json:
                    ret = new JsonResultResolver();
                    break;
                case EMessageType.Xml:
                    ret = new XmlResultResolver();
                    break;
                case EMessageType.Html:
                    ret = new HtmlResultResolver();
                    break;
                case EMessageType.Text:
                    ret = new TextResultResolver();
                    break;
                default:
                    {
                        throw new ArgumentOutOfRangeException("messageType", "出现了意外的消息类型");
                    }
            }
            return ret;
        }
    }
}
