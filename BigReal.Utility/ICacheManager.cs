using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    /// <summary>
    /// 缓存管理器 接口
    /// </summary>
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern);
        void Clear();
    }
}
