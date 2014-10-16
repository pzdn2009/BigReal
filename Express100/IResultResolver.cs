using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Express100
{
    /// <summary>
    /// 结果解析
    /// </summary>
    public interface IResultResolver
    {
        T Deserialize<T>(string str);
        EMessageType MessageType { get; }
    }
}
