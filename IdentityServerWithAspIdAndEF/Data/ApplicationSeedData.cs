using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerWithAspIdAndEF.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerWithAspIdAndEF.Data
{
    public class ApplicationSeedData
    {

        public static async Task EnsureSeedDataAsync(IApplicationBuilder builder)
        {
            Console.WriteLine("Seeding database...");
            using (var scope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //必须优先创建 AspIdentity数据库  后面PersistedGrantDbContext 和 ConfigurationDbContext 需要使用
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appContext.Database.Migrate();
                            

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
             
                //获取用户管理仓储
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                //添加用户数据
                if (!userManager.Users.Any())
                {
                    var systemUser = new ApplicationUser
                    {
                        UserName = "Crashsol",
                        NormalizedUserName = "Crashsol",
                        Email = "47147551@qq.com",
                        Avatar = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1516257337186&di=db41e5611bcdb4eede8979af104b2eeb&imgtype=0&src=http%3A%2F%2Fimg4.99114.com%2Fgroup1%2FM00%2FA3%2F7F%2FwKgGMFVk__WAeRumAAAY72dGo-o180.png"

                    };
                    //创建角色
                    var role = new IdentityRole()
                    {
                        Name = "administrator",
                        NormalizedName = "administrator"
                    };
                    await roleManager.CreateAsync(role);

                    await userManager.CreateAsync(systemUser, "123qwe!@#");
                    //添加用户所属角色
                    await userManager.AddToRoleAsync(systemUser, role.Name);               

                    var userClaims = new Claim[]
                    {
                        new Claim("Permission","User"),
                        new Claim("Permission","User_Create"),
                        new Claim("Permission","User_Update"),
                        new Claim("Permission","User_Read"),
                        new Claim("Permission","User_Delete")
                    };
                    await userManager.AddClaimsAsync(systemUser, userClaims);

                }
                

                //添加Clients ,Resourece 数据库及数据
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                EnsureSeedData(context);



            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }

        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Config.GetClients().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Config.GetIdentityResources().ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Config.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }
        }

    }
}
