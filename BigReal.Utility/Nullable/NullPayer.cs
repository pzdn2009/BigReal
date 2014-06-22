using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPWorkUtility
{
    /// <summary>
    /// 空对象
    /// </summary>
    public class NullPayer : Payer
    {
        public override bool IsNull()
        {
            return true;
        }

        public override void Pay(decimal money)
        {
            //skip do nothing 
        }
    }
}
