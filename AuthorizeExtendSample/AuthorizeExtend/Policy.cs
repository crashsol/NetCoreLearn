using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend
{
    /// <summary>
    /// 策略模型
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        public List<Role> Roles { get; internal set; } = new List<Role>();

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<Permission> Permissions { get; internal set; } = new List<Permission>();

        /// <summary>
        /// 策略检查
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        internal Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            //获取所有角色
            var roles = Roles.Where(b => b.Evaluate(user)).Select(x => x.Name).ToArray();

            //获取所有角色的权限信息
            var permissions = Permissions.Where(b => b.Evaluate(roles)).Select(x => x.Name).ToArray();

            var result = new PolicyResult
            {
                Roles = roles.Distinct(),
                Permissions = permissions.Distinct()
            };
            return Task.FromResult(result);

        }

    }
}
