using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerCenter.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IdentityServerCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            BuildWebHost(args)
               //.MigrateDbContext<ApplicationDbContext>((context, services) => {
               //    new ApplicationDbContextSeed().SeedAsync(context, services)
               //    .Wait();
               //})   //初始化系统数据
               .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(builder =>
                {
                 
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConsole().SetMinimumLevel(LogLevel.Trace);
                })
                .UseUrls("http://localhost:5000")
                .Build();
    }
}
