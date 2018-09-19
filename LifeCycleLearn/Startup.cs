using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LifeCycleLearn.Services;

namespace LifeCycleLearn
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
            services.AddMvc();

            //瞬时Transient(瞬时的)       
            //每次请求时都会创建的瞬时生命周期服务。这个生命周期最适合轻量级，无状态的服务。
            services.AddTransient<ITestService, TestService>();

            //Scoped(作用域的)
            //在同作用域,服务每个请求只创建一次。
            services.AddScoped<ITestService2, TestService2>();

            //单例模式
            //全局只创建一次,第一次被请求的时候被创建,然后就一直使用这一个.
            services.AddSingleton<ITestService3, TestService3>();
        
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
