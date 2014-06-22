using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    /// <summary>
    /// 导出标题显示
    /// </summary>
    public class ExportDisplayAttribute : Attribute
    {
        public ExportDisplayAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }

        public ExportDisplayAttribute(string displayName, int order)
        {
            this.DisplayName = displayName;
            this.Order = order;
        }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 输出名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
