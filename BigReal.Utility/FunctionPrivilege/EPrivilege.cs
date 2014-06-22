using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPWorkUtility
{
    [Flags]
    public enum EPrivilege
    {
        Retrieve = 1 << 0,
        Create = 1 << 1,
        Update = 1 << 2,
        Delete = 1 << 3,
        None = 1 << 4
    }
}
