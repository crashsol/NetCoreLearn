using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HelloCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("1:  first Step    ");
                await next.Invoke();

            });

            app.Use(next =>
            {
                return (context) =>
                {
                    context.Response.WriteAsync("2 : second step    ");
                    return next(context);
                };

            });
          
            app.Run(async (context) =>
            {
             //   await context.Response.WriteAsync($"ConnectString = {configuration["ConnectionStrings:DefaultConnection"]}    ");

                await context.Response.WriteAsync($"name = {configuration["name"]}");

                await context.Response.WriteAsync($"age = {configuration["age"]}");
            });
        }
    }
}
