using System;
using Polly;

namespace PollyLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Polly!");


            {
                Console.WriteLine("处理ArgumentNullException，进行重试3次");
                try
                {
                    var policy = Policy.Handle<ArgumentNullException>().Retry(3, (ex, time) =>
                     {
                         Console.WriteLine($"捕获{nameof(ArgumentNullException)}:{ ex.Message} , 进行第{time}重试");

                     });

                    policy.Execute(() =>
                    {
                        Console.WriteLine($"抛出异常");
                        throw new ArgumentNullException("参数为空");
                    });

                }
                catch (Exception ex)
                {

                    Console.WriteLine("重试3次后,调用失败");
                }
                Console.WriteLine("--------------------------------------------------");
            }

            {
                Console.WriteLine("处理ArgumentNullException，每次重试中间间隔不同时间，进行重试3次");
                try
                {
                    var policy = Policy.Handle<ArgumentNullException>()
                        .WaitAndRetry(3, (num, time) => TimeSpan.FromSeconds(Math.Pow(2,num)),(exception, timeSpan,time, context)=> {
                            Console.WriteLine($"第{time}重试,等待{timeSpan}秒,{context.Count}");
                        });                   

                    policy.Execute(() =>
                    {
                        Console.WriteLine($"抛出异常");
                        throw new ArgumentNullException("参数为空");
                    });

                }
                catch (Exception ex)
                {

                    Console.WriteLine("重试3次后,调用失败");
                }

                Console.WriteLine("--------------------------------------------------");
            }
                    



            Console.ReadLine();
        }




       static void TestMethod(string  a,int b)
        {
            if (string.IsNullOrEmpty(a)) throw new ArgumentException("a 参数不能为空");            

        }
    }
}
