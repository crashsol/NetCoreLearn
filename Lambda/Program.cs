using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("learn Lambda");
            {


                //Action dummyLambda = () =>
                //{
                //    Console.WriteLine("hello world form lambda");
                //};


                //Func<double, double> square = x => x * x;

                //dummyLambda();
                //var result = square(5);
                //Console.WriteLine("result is {0} ",result);


                //DoSomeStuff();



                //dynamic person = null;
                //person = new
                //{
                //    Name = "Jesse",
                //    Age = 18,
                //    Ask = ((Action<string>)((string question) => {
                //        Console.WriteLine("The answer to '" + question + "' is certainly 20. My age is " + person.Age);
                //    }))
                //};
                //person.Ask("asdf");




                //var name = Console.ReadLine();
                //var sentence = Translations.GetSayName(name);
                //Console.WriteLine(sentence("Crashsol"));
            }
            {
                //string[] info = new string[] { "123", "234", "456" };
                //List<Action> listAction = new List<Action>();

                //foreach (var item in info)
                //{
                //    listAction.Add(() => Console.WriteLine(item));
                //}
                //foreach (var item in listAction)
                //{
                //    item.Invoke();
                //}
            }

            {
            //    Thermostat thermostat = new Thermostat();
            //    Heater heater = new Heater(60);
            //    Cooler cooler = new Cooler(80);

            //    thermostat.OnTemperatureChanged += heater.OnTemperatureChanged;
            //    thermostat.OnTemperatureChanged += cooler.OnTemperatureChanged;
            //    Console.WriteLine("请输入温度:");

            //    string temperature = Console.ReadLine();
            //    thermostat.CurrentTemperature = int.Parse(temperature);
            }

            TestBuiltInTypes testBuiltInTypes = new TestBuiltInTypes();
           
            foreach (var item in testBuiltInTypes)
            {
                Console.WriteLine(item);
            }


            Console.ReadLine();
        }

       static void DoSomeStuff()
        {
            var coeff = 10;
            Func<int, int> compute = x => coeff * x;
            Action modifier = () =>
            {
                coeff = 5;
            };

            var result1 = DoMoreStuff(compute);

            Console.WriteLine(result1);

            ModifyStuff(modifier);
            

            var result2 = DoMoreStuff(compute);
            Console.WriteLine(result2);
        }

       static int DoMoreStuff(Func<int, int> computer)
        {
            return computer(5);
        }

       static void ModifyStuff(Action modifier)
        {
            modifier();
        }


        static class Translations
        {
            static readonly Dictionary<string, Func<string, string>> smnFunctions = new Dictionary<string, Func<string, string>>();

            static Translations()
            {
                smnFunctions.Add("fr", name => "Je m'appelle " + name + ".");
                smnFunctions.Add("de", name => "Mein Name ist " + name + ".");
                smnFunctions.Add("en", name => "My name is " + name + ".");
            }

            public static Func<string,string> GetSayName(string language)
            {
                return smnFunctions[language];
            }
        }


        public class TestBuiltInTypes : IEnumerable<string>
        {
            public IEnumerator<string> GetEnumerator()
            {
                yield return "123";
                yield return "444";
                yield return "333";
                yield return "222";
                yield return "123";
                yield return "123";
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }


    }
}
