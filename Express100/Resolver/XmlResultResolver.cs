using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Express100
{
    public class XmlResultResolver : IResultResolver
    {
        public T Deserialize<T>(string str)
        {
            MemoryStream ms = new MemoryStream (Encoding.Default.GetBytes(str));
            return (T)new XmlSerializer(typeof(T)).Deserialize(ms);
        }


        public EMessageType MessageType
        {
            get { return EMessageType.Xml; }
        }
    }
}
