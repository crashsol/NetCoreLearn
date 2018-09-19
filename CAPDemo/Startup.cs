using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPDemo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DotNetCore.CAP;
using CAPDemo.Service;

namespace CAPDemo
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
            //添加数据库
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseMySQL(Configuration.GetConnectionString("MysqlUser"));

            });

            #region 添加CAP相关配置
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(option =>
            {
                //添加数据库连接配置
                option.UseEntityFramework<ApplicationDbContext>();

                //配置RabbitMQ
                option.UseRabbitMQ(Configuration.GetConnectionString("RabbitMQ"));
                //启用DashBoard
                option.UseDashboard();
                // 注册节点到 Consul
                option.UseDiscovery(d =>
                {
                    d.DiscoveryServerHostName = "localhost";
                    d.DiscoveryServerPort = 8500;
                    d.CurrentNodeHostName = "localhost";
                    d.CurrentNodePort = 5800;
                    d.NodeId = 1;
                    d.NodeName = "CAP No.1 Node";
                });

            });
          

            #endregion


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCap();

            app.UseMvc();
        }
    }
}
