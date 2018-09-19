using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend.AspNetCore
{

    /// <summary>
    /// 自定义授权服务Bulider,用于注册服务
    /// </summary>
    public class PolicyServerBuilder
    {
        public IServiceCollection Services { get; }

        public PolicyServerBuilder(IServiceCollection serviceCollection)
        {
            Services = serviceCollection;
        }


        /// <summary>
        /// 注册PolicyServer 授权服务
        /// </summary>
        /// <returns></returns>
        public PolicyServerBuilder AddAuthorizationPermissionPolicies()
        {
            Services.AddAuthorization();
            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();            
            Services.AddTransient<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            //使用自定义授权策略
            Services.AddTransient<IAuthorizationHandler, PermissionHandler>();

            return this;
        }

    }
}
