using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AuthorizeLearn.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using AuthorizeLearn.Service;
using AuthorizeLearn.Data;

namespace AuthorizeLearn
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
            //添加Cookies 认证配置
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie();

            services.AddAuthorization(option =>
            {
                //声明是授权，添加一个policy ，根据用户Claims 判读是否包含 Claim.Type == "Permission"
                option.AddPolicy("Permission", builder => builder.RequireClaim("Permission"));
                option.AddPolicy("Contact", builder => builder.RequireClaim("Contact"));

                //基于策略是授权（基于RequireMent）  限制用户年龄最小为21
                option.AddPolicy("AtLeast18", policy =>
                      policy.Requirements.Add(new MinimumAgeRequirement(18)));

                option.AddPolicy("AtLeast20", policy =>
                       policy.Requirements.Add(new MinimumAgeRequirement(20)));


                //添加基于资源授权，主要用于在方法体内部检查，当前用户是否具有某个具体资源的权限（例如Document）
                option.AddPolicy("DocumentPolicy", policy =>policy.Requirements.Add(new DocumentRequirement()));

            });

            services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();        //添加 Requirement资源的处理函数



            services.AddSingleton<IAuthorizationHandler, DocumentAuthorizationHandler>();        //添加资源授权处理工具  
            services.AddScoped<IDocumentRepository, DocumentRepository>();                       //mock document仓储







            #region 添加Jwt 认证配置


            ////添加Jwt 认证配置
            //services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));   //将Jwt配置注入到容器

            //var jwtSetting = new JwtSetting();
            //Configuration.GetSection("JwtSetting").Bind(jwtSetting);                  //获取jwt配置，方便services配置中使用

            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,o => 
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey)),  //  添加验证秘钥
            //        ValidateIssuer = true,
            //        ValidIssuer = jwtSetting.Issuer,                                                            //颁发者
            //        ValidateAudience = true,
            //        ValidAudience = jwtSetting.Audience,                                                        //客户
            //        ValidateLifetime = true,                        //validate the expiration and not before values in the token
            //        ClockSkew = TimeSpan.FromMinutes(30) //5 minute tolerance for the expiration date

            //    }              
            // );
            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();   //启用认证

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
