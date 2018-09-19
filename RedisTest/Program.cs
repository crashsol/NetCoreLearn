
using RedisTest.Cache;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Redis 测试!");

            try
            {
                RedisHelper redisHelper = new RedisHelper("test", "localhost");

                {
                    //Console.WriteLine("************* Redis String ********************");
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    redisHelper.StringSet("string:" + i.ToString(), "Value:" + DateTime.Now.ToString());
                    //}
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    Console.WriteLine(redisHelper.StringGet("string:" + i.ToString()));
                    //}

                }

                {


                    //Console.WriteLine("************* Redis Hash ********************");
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    redisHelper.HashSet(i.ToString(), "Id", i.ToString());
                    //    redisHelper.HashSet(i.ToString(), "Age",  (i +3).ToString());
                    //    redisHelper.HashSet(i.ToString(), "Name", "Name"+i.ToString());                    
                    //}

                    //for (int i = 0; i < 5; i++)
                    //{
                    //    redisHelper.HashSet(i.ToString(), "Id", (3+i).ToString());
                    //    redisHelper.HashSet(i.ToString(), "Age", (i +5).ToString());
                    //    redisHelper.HashSet(i.ToString(), "Name", "Name" + i.ToString());
                    //}
                    //redisHelper.HashSet("1", "Id", "0000000000");
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    //获取所有属性
                    //    foreach (var item in redisHelper.HashKeys(i.ToString()))
                    //    {
                    //        //获取所有属性的值
                    //        Console.WriteLine($"{item} :{ redisHelper.HashGet(i.ToString(),item)}");
                    //    }
                    //}
                }

                {

                    Console.WriteLine("************* Redis Set ********************");
                    //for (int i = 0; i < 5; i++)
                    //{
                    //    var entity = new Student
                    //    {
                    //        Id = i,
                    //        Name = "Student" + i.ToString(),
                    //        Age = 1 + 22,

                    //    };
                    //    redisHelper.SortedSetAdd("Student",entity,0);
                    //}
                    var binaryFormatter = new BinaryFormatter();
                    var dtos = redisHelper.SortedSetRangeByRank("Student", 0, -1);
                    Console.WriteLine($"元素个数: {dtos.Count()}");
                    foreach (var item in dtos)
                    {


                    }
                }





            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ ex.Message}");
            }
            Console.ReadLine();



        }
    }
}
