using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerWithAspIdAndEF.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerWithAspIdAndEF.Services
{
    public class ProfileService : IProfileService
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public readonly RoleManager<IdentityRole> _roleManager;

        public ProfileService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        private async Task<List<Claim>> GetClaimsFromUser(ApplicationUser user)
        {

            //添加用户信息 
            var claims = new List<Claim>() {
                new Claim(JwtClaimTypes.Subject,user.Id),
                new Claim(JwtClaimTypes.Name,user.UserName)
            };
            //添加头像
            if(!string.IsNullOrWhiteSpace(user.Avatar))
            {
                claims.Add(new Claim("avatar", user.Avatar));
            }
            //添加用户所属角色信息
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }
            return claims;
            
        }

        /// <summary>
        /// 返回用户信息 Claims
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            if (subjectId != null)
            {
                var user = await _userManager.FindByIdAsync(subjectId);
                if(user !=null)
                {
                   if(user.LockoutEnabled)
                    {
                        //组合要返回的Claims
                        context.IssuedClaims = await GetClaimsFromUser(user);
                    }
                }
            }       


        }
        

        /// <summary>
        /// 判断用户是否有效
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = false;

            var subjectId = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            if(subjectId !=null)            {
                var user = await _userManager.FindByIdAsync(subjectId);
                if(user!=null)
                {
                    if(user.LockoutEnabled)
                    {
                        context.IsActive = true;
                    }
                }

            }

        }
    }
}
