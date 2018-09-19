using AuthorizeExtendSample.AuthorizeExtend.PolicyServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizeExtendSample.AuthorizeExtend.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the policy server client.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static PolicyServerBuilder AddPolicyServerClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Policy>(configuration);
            services.AddTransient<IPolicyServerClient, PolicyServerClient>();
            services.AddScoped(provider => provider.GetRequiredService<IOptionsSnapshot<Policy>>().Value);

            return new PolicyServerBuilder(services);
        }
    }
}
