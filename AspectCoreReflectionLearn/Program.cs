using System;
using System.Diagnostics;
using System.Reflection;
using AspectCore.Extensions.Reflection;

namespace AspectCoreReflectionLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var test = typeof(Test).GetTypeInfo().GetProperty("Name").GetCustomAttributes(typeof(CustomerAttribute), true);
            var ddd = test as CustomerAttribute[];

            sw.Stop();
            Console.WriteLine($"原始方法调用时间: { sw.ElapsedMilliseconds}");
            sw.Start();

            var reflector = typeof(Test).GetTypeInfo().GetProperty("Name").GetReflector();
            var attributes = reflector.GetCustomAttributes(typeof(CustomerAttribute));

            sw.Stop();
            Console.WriteLine($"AspectCore Reflcetor 方法调用时间: { sw.ElapsedMilliseconds}");

            Console.ReadLine();
        }
    }
}
