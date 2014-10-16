using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Express100
{
    public class JsonResultResolver : IResultResolver
    {
        public T Deserialize<T>(string str)
        {
            var jser = new JavaScriptSerializer();
            return jser.Deserialize<T>(str);
        }


        public EMessageType MessageType
        {
            get { return EMessageType.Json; }
        }
    }
}
