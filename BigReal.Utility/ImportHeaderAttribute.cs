using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportHeaderAttribute : Attribute
    {
        public ImportHeaderAttribute(string displayName)
            : this(displayName, true)
        {

        }

        public ImportHeaderAttribute(string displayName, bool nullable)
            : this(displayName, nullable, null)
        {

        }

        public ImportHeaderAttribute(string displayName, bool nullable, object defautValue)
        {
            this.Nullable = nullable;
            this.DisplayName = displayName;
            this.DefaultValue = defautValue;
        }

        /// <summary>
        /// 是否允许为空：true表示可以，默认值。false，则不允许为空。
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 输出名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; set; }
    }
}
