using System;
using System.Collections.Generic;
using System.Threading;
using MongoLearn.Data;
using MongoLearn.Model;
using Autofac;

namespace MongoLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mongo 测试！");
        
            IContactBookRepository contactBookRepository = new MongoContactBookRepository(new ContactContext("mongodb://127.0.0.1:27017", "UserContact"));

            {
                //var result = contactBookRepository.AddContactUserAsync(1, new ContactUser
                //{
                //    Id = 3,
                //    Name = "学生2",
                //    Title = "五道口学校",
                //    Company = ""
                //}, new CancellationToken()).Result;

                //if (result)
                //    Console.WriteLine("添加信息成功");
                //Console.ReadLine();
            }

          var result =  contactBookRepository.TagContactUserAsync(1, 2, new List<string> { "好朋友", "学霸","adsf" }, new CancellationToken()).Result;

            if(result)
                Console.WriteLine("给好友打标签成功");

            Console.ReadLine();
         
        }
    }
}
