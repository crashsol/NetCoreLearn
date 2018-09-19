using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;


namespace AspectCoreDemo.Infrastructure
{
    public class CustomInterceptorAttribute : AbstractInterceptorAttribute
    {
        

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {
                var logger = context.ServiceProvider.GetService<ILogger<CustomInterceptorAttribute>>();
                //方法调用之前
                logger.LogInformation("Before the Service call...");

                await next(context);

                logger.LogInformation("After the Service call...");            
                                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
