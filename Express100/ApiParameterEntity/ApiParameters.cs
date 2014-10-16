using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Express100.ApiParameterEntity
{
    /// <summary>
    /// Api参数实体
    /// </summary>
    public class ApiParameters : ParameterBase
    {
        /// <summary>
        /// 快递的电话号码，只有佳吉物流需要这个参数，其他忽略
        /// </summary>
        public string ValiCode { get; set; }

        /// <summary>
        /// 是否多行
        /// </summary>
        public bool Mutiline { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public ESortWay Order { get; set; }

        public override string ToUrl()
        {
            var format = "http://api.kuaidi100.com/api?id={0}&com={1}&nu={2}&valicode={3}&show={4}&muti={5}&order={6}";
            return string.Format(format, Key, CompanyCode, ExpressNumber, ValiCode, (int)Show, Mutiline ? "1" : "0", Order.ToString().ToLower());
        }
    }
}
