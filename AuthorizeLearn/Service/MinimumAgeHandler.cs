using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizeLearn.Service
{

    /// <summary>
    /// 基于策略 授权处理
    /// </summary>
    public class MinimumAgeHandler:AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            //验证用户 ClaimsType 中是否包含 DateOfBirth
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                //验证失败
                return Task.CompletedTask;
                
            }

            //获取时间
            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

            //计算年龄
            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }
            if (calculatedAge >= requirement.MinimumAge)
            {
                //验证成功
                context.Succeed(requirement);
            }

            //TODO: Use the following if targeting a version of
            //.NET Framework older than 4.6:
            //      return Task.FromResult(0);
            return Task.CompletedTask;


        }
    }
}
