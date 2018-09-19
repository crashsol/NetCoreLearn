using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiddlewareConsole
{
    class Program
    {
        public static List<Func<RequestDeleagte, RequestDeleagte>> _list = new List<Func<RequestDeleagte, RequestDeleagte>>();
        static void Main(string[] args)        {


            Console.WriteLine("模拟构建管道!");

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("1...");
                    return next.Invoke(context);
                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("2...");
                    return next.Invoke(context);
                };
            });

            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("3...");
                    return next.Invoke(context);
                };
            });

            RequestDeleagte end = (context) =>
            {
                Console.WriteLine("End ....");
                return Task.CompletedTask;
            };

             _list.Reverse();
            foreach (var middlerware in _list)
            {
                end = middlerware.Invoke(end);
            }

            end.Invoke(new Context());


            Console.ReadLine();
        }

        static void Use(Func<RequestDeleagte, RequestDeleagte> middleware)
        {
            _list.Add(middleware);
        }
    }
}
