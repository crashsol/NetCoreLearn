using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeLearn.Models;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizeLearn.Service
{

    /// <summary>
    /// 资源授权处理工具 基于部门信息、用户列表的权限授权检查
    /// </summary>
    public class DocumentAuthorizationHandler :AuthorizationHandler<DocumentRequirement, Document>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DocumentRequirement requirement,Document resource)
        {
            var userDepartment = context.User.Claims.FirstOrDefault(c => c.Type == "Depart")?.Value; //获取用户所在部门

            var userName = context.User.Identity?.Name;   //获取用户名称


            //如果用户具有该资源的访问权限，授权通过
            if(resource.AllowUsers.Contains(userName) || resource.Departments.Contains(userDepartment))
            {
                context.Succeed(requirement);
            }         
         

            return Task.CompletedTask;
        }
    }
    public class DocumentRequirement : IAuthorizationRequirement { }


}
