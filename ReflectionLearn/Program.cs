#define CONDITION_A

using DB.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;




namespace ReflectionLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************************");         

            {
                Assembly assembly = Assembly.Load("DB.Sqlserver");
                Type typeHelper = assembly.GetType("DB.Sqlserver.SqlserverHelper");

                var sqlHelper = Activator.CreateInstance(typeHelper) as IDBHelper;
                sqlHelper.Query();

            }            
            { 

                /////GetType(); 实例对象可以调用该方法
                //DateTime dateTime = new DateTime();
                //Type type = dateTime.GetType(); //只有实例才能调用该方法，例如静态类是无法实例化，没办法调用该方法
                //foreach (var property in type.GetProperties())
                //{
                //    Console.WriteLine(property.Name);
                //}
                ////Console.WriteLine("------------------------------------");
            }

            {

                /////typeof();  在编译时绑定到特定的Type实例              
                //Type type = typeof(DateTime); //只有实例才能调用该方法，例如静态类是无法实例化，没办法调用该方法
                //foreach (var property in type.GetProperties())
                //{
                //    Console.WriteLine(property.Name);
                //}
                //Console.WriteLine("------------------------------------");
            }

            {
                ////判断类或则方法是否支持泛型
                //Type type;
                //type = typeof(System.Nullable<>);
                //Console.WriteLine(type.ContainsGenericParameters);
                //Console.WriteLine(type.IsGenericType);

                //type = typeof(Nullable<DateTime>);
                //Console.WriteLine(type.ContainsGenericParameters);
                //Console.WriteLine(type.IsGenericType);


                ////获取泛型类参数
                //type = typeof(Stack<int>);
                //foreach (var item in type.GetGenericArguments())
                //{
                //    Console.WriteLine($"Type parameter: {type.FullName}");
                //}
            }

            {
                Type type = typeof(CommandLineInfo);
                PropertyInfo propertyInfo = type.GetProperty("Help");
                MyAttribute myAttribute =(MyAttribute) propertyInfo.GetCustomAttributes(typeof(MyAttribute), false)[0];

                Console.WriteLine($"Help's Attribute is: {myAttribute.Alise}");

               var attribute = myAttribute.GetType().GetCustomAttributes(typeof(AttributeUsageAttribute));

                foreach (var item in attribute)
                {
                    AttributeUsageAttribute aa = item as AttributeUsageAttribute;
                    Console.WriteLine($"{ aa.AllowMultiple }  {aa.Inherited}  {aa.TypeId}");

                }



            }


            {
                FileInfo fileInfo = new FileInfo(@"1.txt");
                fileInfo.Attributes = FileAttributes.Hidden | FileAttributes.ReadOnly;

                Console.WriteLine($"{fileInfo.Attributes.ToString().Replace(",","|")} outputs as {fileInfo.Attributes}");
            }

            {
                Console.WriteLine("-----------CONDITION  BEGIN-------------");
                Method_A();
                Method_B();
                Console.WriteLine("-----------CONDITION  End-------------");

            }
     
            Console.ReadLine();
        }
        [Conditional("CONDITION_A")]  //根据环境定义的变量进行调用， #define CONDITION_A  类似#if #endif
        static void Method_A()
        {
            Console.WriteLine("Method_A --------");
        }

        [Conditional("CONDITION_B")]
        static void Method_B()
        {
            Console.WriteLine("Method_B --------");
        }

        public static void PrintMembers(MemberInfo[] ms)
        {
            foreach (var item in ms)
            {
                Console.WriteLine("{0}{1}", "       ", item);
            }
            Console.WriteLine("----------------------------------");
        }

        public class CommandLineInfo
        {
            [My("my Attribute Info")]
            public bool Help { get; set; }

            public string Out { get; set; }

            private ProcessPriorityClass _priority = ProcessPriorityClass.Normal;


            public ProcessPriorityClass Priority
            {
                get
                {
                    return _priority;
                }
                set
                {
                    _priority = value;
                }
            }                    
        }
        [AttributeUsage(AttributeTargets.Property)]
        public class MyAttribute:Attribute
        {
            public string Alise { get; set; }

            public MyAttribute(string alise)
            {
                Alise = alise;
            }
        }


    }
}
