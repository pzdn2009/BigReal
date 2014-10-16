using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Express100
{
    public abstract class ParameterBase
    {
        /// <summary>
        /// 身份授权Key
        /// </summary>
        public virtual string Key { get; set; }
        /// <summary>
        /// 公司代码
        /// </summary>
        public virtual string CompanyCode { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public virtual string ExpressNumber { get; set; }
        /// <summary>
        /// 返回的消息类型
        /// </summary>
        public virtual EMessageType Show { get; set; }

        public abstract string ToUrl();
    }
}
