using AuthorizeExtendSample.AuthorizeExtend.PolicyServer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend.AspNetCore.Middleware
{
    /// <summary>
    /// Middleware to automatically turn application roles and permissions into claims
    /// </summary>
    public class PolicyServerClaimsMiddleware
    {

        private readonly RequestDelegate _next;
        public PolicyServerClaimsMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IPolicyServerClient client)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                //如果用户已经通过认证
                var policy = await client.EvaluateAsync(context.User);
                //获取所有 角色列表
                var roleClaims = policy.Roles.Select(b => new Claim("role", b));
                //权限列表
                var permissionClaims = policy.Permissions.Select(b => new Claim("permission", b));
                var id = new ClaimsIdentity("PolicyServerMiddleware", "name", "role");
                id.AddClaims(roleClaims);
                id.AddClaims(permissionClaims);
                context.User.AddIdentity(id);

            }

        }



    }
}
