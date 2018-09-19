using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend.PolicyServer
{
    /// <summary>
    /// 策略服务实现
    /// </summary>
    public class PolicyServerClient : IPolicyServerClient
    {
        private readonly Policy _policy;

        public PolicyServerClient(Policy policy)
        {
            _policy = policy;
        }

        /// <summary>
        /// 获取用户所有角色及权限信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return await _policy.EvaluateAsync(user);
        }

        public async Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission)
        {
            //获取用户所有的角色和权限信息
            var policy = await EvaluateAsync(user);

            return policy.Permissions.Contains(permission);
        }

        /// <summary>
        /// 检查用户是否属于某个角色
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="role">角色名称</param>
        /// <returns></returns>
        public async Task<bool> IsInRoleAsync(ClaimsPrincipal user, string role)
        {
            // 获取用户所有的角色和权限信息
            var policy = await EvaluateAsync(user);
            return policy.Roles.Contains(role);
        }
    }
}
