using System;
using System.Linq;
using System.Data;
namespace LinqLearn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Learn Linq");


            {
                Console.WriteLine("----Group by---");
                //var numbersgroup = from b in TestHelper.CreateNumbersTable()
                //                   group b by b % 5 into g
                //                   select new { Remainder = g.Key, Numbers = g };
                //foreach (var item in numbersgroup)
                //{
                //    Console.WriteLine($" Numbers with a remainder of { item.Remainder}  when divided by 5");
                //    foreach (var number in item.Numbers)
                //    {
                //        Console.WriteLine($"{number}");
                //    }
                //}

                //lambda
                var group1 = TestHelper.CreateNumbersTable().GroupBy(b => b % 5).Select(a => new { a.Key,arr =a.ToList() });

                foreach (var item in group1)
                {
                    Console.WriteLine($"Numbers with a remainder of { item.Key}  when divided by 5");
                    foreach (var item1 in item.arr)
                    {
                        Console.WriteLine(item1);
                    }
                }

            }

            {
                Console.WriteLine("left join");
                var customersOrders = TestHelper.GetCustomers().
                                    Join(TestHelper.GetOrders(), c => c.OrderId, o => o.Id, (c, o) => new { c, o })
                                    .Select(g => new
                                    {
                                        CustomerId =  g.c.Id,
                                        CustomerName =g.c.Name,
                                        CustomerAddress = g.c.Address,
                                        Price = g.o.Total,
                                        CreateTime = g.o.OrderDate
                                    });

                foreach (var item in customersOrders)
                {
                    Console.WriteLine($"{item.CustomerId} -- {item.CustomerName } --{item.CustomerAddress} --{ item.Price} --{item.CreateTime}");
                }

            }


            {
                Console.WriteLine("Group join");
                var customersOrders = TestHelper.GetCustomers().
                                    GroupJoin(TestHelper.GetOrders(), c => c.OrderId, o => o.Id, (c, o) => new { c, o })
                                    .Select(o => o ).ToList();

                foreach (var item in customersOrders)
                {
                    Console.WriteLine($"{item.c.Id} -- {item.c.Name } --{item.c.Address} --{item.o.SingleOrDefault()?.Id}");
                }

            }


            Console.ReadLine();
        }
    }
}
