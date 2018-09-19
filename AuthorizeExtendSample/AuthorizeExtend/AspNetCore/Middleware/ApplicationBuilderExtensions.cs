using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend.AspNetCore.Middleware
{
    /// <summary>
    /// PolicyServer 管道扩展
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用 PolicyServer管道
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UsePolicyServerClaims(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PolicyServerClaimsMiddleware>();
        }
    }
}
