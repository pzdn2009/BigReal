using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigReal.Utility.Extensions
{
    public static class BooleanExtensions
    {
        public static int ToBinary(this bool b)
        {
            return b ? 1 : 0;
        }

        public static bool IfTrue(this bool b, Action action)
        {
            if (b)
            {
                action();
            }
            return b;
        }

        public static bool IfFalse(this bool b, Action action)
        {
            if (!b)
            {
                action();
            }
            return b;
        }
    }
}
