using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPWorkUtility
{
    /// <summary>
    /// 功能权限实体
    /// </summary>
    public partial class Privilege
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 功能菜单ID
        /// </summary>
        public string FunctionMenuID { get; set; }

        /// <summary>
        /// 权限位标记
        /// </summary>
        public int Flag { get; set; }
    }

    public partial class Privilege
    {
        /// <summary>
        /// 将Flag与EPrivilege等效转化
        /// </summary>
        public EPrivilege FlagEqualToEPrivilege
        {
            get
            {
                var str = this.Flag.ToString();
                return (EPrivilege)Enum.Parse(typeof(EPrivilege), str, true);
            }
            set
            {
                this.Flag = (int)value;
            }
        }

        public bool HasPrivilege(EPrivilege privilege)
        {
            return (this.FlagEqualToEPrivilege & privilege) != 0;
        }

    }
}
