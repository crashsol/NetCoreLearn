using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using IdentityServerCenter.Data;
using Microsoft.EntityFrameworkCore;
using IdentityServerCenter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using IdentityServerCenter.Services;

namespace IdentityServerCenter
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
            #region 本地认证配置
            //添加数据库配置
            //services.AddDbContext<ApplicationDbContext>(option =>
            //{
            //    option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //});

            ////添加 UserManger 等依赖注入
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //  .AddEntityFrameworkStores<ApplicationDbContext>()
            //  .AddDefaultTokenProviders();

            ////添加认证 Cookies
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => {
            //        options.LoginPath = "/Account/Login";
            //    });
            ////添加认证 登录用户  用户名，密码设置
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 6;
            //});
            #endregion            

            //添加MVC依赖注入
            services.AddMvc();

            #region IdentityServer OpenID Connect 服务端配置
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())   //配置可访问的 resources
                .AddInMemoryClients(Config.GetClients())            //配置可访问的 clients 
                .AddTestUsers(Config.GetTestUsers())                //添加 内存测试用户，输入 用户名、密码进行认证
                .AddInMemoryIdentityResources(Config.GetIdentityResources());     //添加认证资源               

            #endregion


            //添加Consent同意服务
            services.AddScoped<ConsentService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //移除处理中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //启用identityServer 中间件
            app.UseIdentityServer();

            //启用本地认证中间件
            //app.UseAuthentication();      

            //启用静态资源中间件
            app.UseStaticFiles();

            //启用MVC中间件
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
