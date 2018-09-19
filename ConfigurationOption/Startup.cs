using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using ConfigurationOption.Options;

namespace ConfigurationOption
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private IConfiguration Configuration;


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //添加依赖注入
            services.Configure<Class>(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    var myClass = new Class();
            //    Configuration.Bind(myClass);

            //    await context.Response.WriteAsync($"ClassNo:{myClass.ClassNo} ClassDesc:{myClass.ClassDesc}     ");
            //    await context.Response.WriteAsync($"    Students Count :{myClass.Students.Count}   ");

            //    foreach (var item in myClass.Students)
            //    {
            //        await context.Response.WriteAsync($"   Name:{item.Name}  Age :{item.Age}");
            //    }

            //});

            app.UseMvcWithDefaultRoute();
        }
    }
}
