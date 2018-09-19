using AspectCore.DynamicProxy;
using AspectCoreDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspectCoreDemo.Services
{
    [NonAspect]
    public interface  IUserService
    {
        //[CustomInterceptor]       
        Task<string> GetUserName();
    }
}
