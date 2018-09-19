using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServerClientMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Cookies";   //配置认证表
                option.DefaultChallengeScheme = "oidc";         // when we need the user to login, we will be using the OpenID Connect scheme.
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";   //登录后的配置表
                options.Authority = "http://localhost:5000";  //远程登录验证地址
                options.RequireHttpsMetadata = false;     //是否启用https


                //使用OpenId Connect添加用户认证（implicit flow，简化流程）
                //options.ClientId = "ImplicitMVC";
                //options.ClientSecret = "secret";

                //从id_token中获取客户信息
                options.GetClaimsFromUserInfoEndpoint = true;
                options.ClaimActions.MapJsonKey("sub", "sub");
                options.ClaimActions.MapJsonKey("avatar", "avatar");
                options.ClaimActions.MapJsonKey("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "role");


                //混合模式客户端
                options.ClientId = "MVC";                 //客户端名称
                options.ClientSecret = "secret";          //客户端密码
                options.ResponseType = OidcConstants.ResponseTypes.CodeIdToken;   //返回的数据类型

                options.Scope.Add("api");                 //要获取的授权资源，必须与Center 给的资源一致
                options.Scope.Add("offline_access");      //获取离线使用this allows requesting refresh tokens for long lived API access:
                options.SaveTokens = true;                //是否保存“Cookies”到本地，由中间件管理

            });


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
