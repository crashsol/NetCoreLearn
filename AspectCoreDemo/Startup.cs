using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using AspectCoreDemo.Infrastructure;
using AspectCoreDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspectCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {


            //注入IUserService
            services.AddTransient<IUserService, UserService>();
            services.AddMvc();

            //注入CustomerInterceptorAttribute 
            //services.AddDynamicProxy();
            //全局拦截器注册:
            //services.AddDynamicProxy(option =>
            //{
            //    option.Interceptors.AddTyped<CustomInterceptorAttribute>();
            //});

            // 作用于特定Service或Method的全局拦截器，下面的代码演示了作用于带有Service后缀的类的全局拦截器：
            //services.AddDynamicProxy(config =>
            //{
            //    config.Interceptors.AddTyped<CustomInterceptorAttribute>(method => method.DeclaringType.Name.EndsWith("Service"));
            //});



            //同时支持全局忽略配置，亦支持通配符：
            services.AddDynamicProxy(config =>
            {
                //App1命名空间下的Service不会被代理
                config.NonAspectPredicates.AddNamespace("App1");

                //最后一级为App1的命名空间下的Service不会被代理
                config.NonAspectPredicates.AddNamespace("*.App1");

                //ICustomService接口不会被代理
                config.NonAspectPredicates.AddService("ICustomService");

                //后缀为Service的接口和类不会被代理
                config.NonAspectPredicates.AddService("*Service");

                //命名为Query的方法不会被代理
                config.NonAspectPredicates.AddMethod("Query");

                //后缀为Query的方法不会被代理
                config.NonAspectPredicates.AddMethod("*Query");
            });
            return services.BuildAspectCoreServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
