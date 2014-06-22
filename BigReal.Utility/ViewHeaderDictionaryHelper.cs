using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    /// <summary>
    /// 获取自定义视图（如数据库表联接的结果）的Dictionary表示
    /// </summary>
    public class ViewHeaderDictionaryHelper
    {
        /// <summary>
        /// 按照属性的默认顺序返回字典
        /// </summary>
        /// <typeparam name="TType">视图类型，本系统中一般以Info结尾</typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeaderDict<TType>()
        {
            return GetHeaderDict<TType>(false);
        }

        /// <summary>
        /// 按照属性的默认顺序返回字典
        /// </summary>
        /// <typeparam name="TType">视图类型，本系统中一般以Info结尾</typeparam>
        /// <param name="useOrder">是否使用Order来规范顺序</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeaderDict<TType>(bool useOrder)
        {
            var dic = new Dictionary<string, ExportDisplayAttribute>();

            var type = typeof(TType);
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                ExportDisplayAttribute cusAttr = null;
                var first = prop.GetCustomAttributes(typeof(ExportDisplayAttribute), false).FirstOrDefault();
                if (first != null)
                {
                    cusAttr = first as ExportDisplayAttribute;
                    if (cusAttr != null)
                    {
                        dic.Add(prop.Name, cusAttr);
                    }
                }
            }
            if (useOrder)
            {
                dic = dic.OrderBy(d => d.Value.Order).ToDictionary(d => d.Key, d => d.Value);
            }

            return dic.ToDictionary(key => key.Key, val => val.Value.DisplayName);
        }
    }
}
