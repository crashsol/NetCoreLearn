using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace ConfigurationCommandLine
{
    class Program
    {
        static void Main(string[] args)
        {

            var settting = new Dictionary<string, string>
            {
                { "name","123"},
                {"age","27"}
            };      
            
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(settting)
                .AddCommandLine(args);                    //添加控制台配置   需要在调试中添加参数，例如 name=crashsol age=30

            var configuration = builder.Build();

            Console.WriteLine($"name:{ configuration["name"]}");   
            Console.WriteLine($"age:{ configuration["age"]}");

            Console.ReadLine();


        }
    }
}
