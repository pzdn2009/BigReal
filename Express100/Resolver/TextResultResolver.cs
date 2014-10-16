using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Express100
{
    public class TextResultResolver : IResultResolver
    {
        public T Deserialize<T>(string str)
        {
            throw new NotImplementedException();
        }

        public EMessageType MessageType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
