using System;
using Microsoft.Extensions.Configuration;

namespace ConfigurationJson
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder().AddJsonFile("Class.json");

            var configuration = builder.Build();


            Console.WriteLine($"ClassNo:{configuration["ClassNo"]}");
            Console.WriteLine($"ClassDesc:{configuration["ClassDesc"]}");


            Console.WriteLine($"********************Studens********");

            Console.WriteLine($"Name:{configuration["Students:0:name"]} age:{configuration["Students:0:age"]}");
            Console.WriteLine($"Name:{configuration["Students:1:name"]} age:{configuration["Students:1:age"]}");
          //  Console.WriteLine($"Name:{configuration["Students:2:name"]} age:{configuration["Students:2:age"]}");

          


            Console.ReadLine();
        }
    }
}
