using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend.PolicyServer
{
    /// <summary>
    /// 策略服务接口
    /// </summary>
    public interface IPolicyServerClient
    {
        /// <summary>
        /// 获取用户所有角色及权限信息
        /// </summary>
        /// <param name="user">用户Claims</param>
        /// <returns></returns>
        Task<PolicyResult> EvaluateAsync(ClaimsPrincipal user);

        /// <summary>
        /// 检查 <paramref name="user"/> 用户,是否具 <paramref name="permission"/> 权限
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="permission">权限名称</param>
        /// <returns></returns>
        Task<bool> HasPermissionAsync(ClaimsPrincipal user, string permission);

        /// <summary>
        /// 检查<paramref name="user"/>用户,是否具有 <paramref name="role"/> 角色
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> IsInRoleAsync(ClaimsPrincipal user, string role);
    }
}
