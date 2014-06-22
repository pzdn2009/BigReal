using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace HPWorkUtility
{
    public static class PrivilgeExtensions
    {
        public static EPrivilege GetPrivilegeType(string actionName)
        {
            if (actionName.StartsWith("new", StringComparison.OrdinalIgnoreCase))
            {
                return EPrivilege.Create;
            }
            else if (actionName.StartsWith("edit", StringComparison.OrdinalIgnoreCase))
            {
                return EPrivilege.Update;
            }
            else if (actionName.StartsWith("delete", StringComparison.OrdinalIgnoreCase))
            {
                return EPrivilege.Delete;
            }
            else
            {
                return EPrivilege.Retrieve;
            }
        }

        public static bool HasPrivilege(this IPrincipal user, string resourceName, EPrivilege privilegeType)
        {
            return false;
        }
    }
}
