using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 认证的用户ID列表
        /// </summary>
        public List<string> Subjects { get; internal set; } = new List<string>();


        /// <summary>
        /// 角色认证列表
        /// </summary>
        public List<string> IdentityRoles { get; internal set; } = new List<string>();

        /// <summary>
        /// 判断 user 是否具有该角色权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal bool Evaluate(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var sub = user.FindFirst("sub")?.Value;
            if (!String.IsNullOrWhiteSpace(sub))
            {
                //如果角色SubIds中存在该用户的ID，返回成功
                if (Subjects.Contains(sub)) return true;
            }
            //获取用户的所有角色信息
            var roles = user.FindAll("role").Select(b => b.Value);
            if(roles.Any())
            {
                //如果该用户的角色，出现在角色的认证列表中，返回成功
                if (IdentityRoles.Any(b => roles.Contains(b))) return true; 
            }
            return false;

        }

    }
}
