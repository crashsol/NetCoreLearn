using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend
{

    /// <summary>
    /// 权限model
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 权限所属的角色列表
        /// </summary>
        public List<string> Roles { get; internal set; } = new List<string>();


        /// <summary>
        /// 诊断该权限实体角色 是否存在于<paramref name="roles"/> 中
        /// </summary>
        /// <param name="roles">角色列表</param>
        /// <returns></returns>
        internal bool Evaluate(IEnumerable<string> roles)
        {
            if (roles == null) throw new ArgumentNullException(nameof(roles));
            if (Roles.Any(b => roles.Contains(b))) return true;
            return false;
        }

    }
}
