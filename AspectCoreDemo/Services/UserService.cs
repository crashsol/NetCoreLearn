using AspectCore.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspectCoreDemo.Services
{
    
    public class UserService : IUserService
    {
        public Task<string> GetUserName()
        {
            Console.WriteLine($"GetUserName Method Is Called");
            return Task.FromResult("Crashsol");
        }
    }
}
