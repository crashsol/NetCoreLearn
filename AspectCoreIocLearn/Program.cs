using System;
using AspectCore.Injector;

namespace AspectCoreIocLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceContainer services = new ServiceContainer();
            //使用类型注册服务
            services.AddType<ILogger, Logger>();

            services.AddType<IRepository, Repository>();
            services.AddType<ISampleService, SampleService>();

            //创建服务解析器
            var resolver = services.Build();

            //根据Type获取实例
            var logger = resolver.Resolve<ILogger>();       

            //获取并检查是否为null，如果获取结果为null，抛出异常
            var respository = resolver.ResolveRequired<IRepository>();
            var sample = resolver.ResolveRequired<ISampleService>();

            logger.LogInfo("获取成功");
            sample.Test();
            respository.Test();

            Console.ReadLine();
          

        }
    }
}
