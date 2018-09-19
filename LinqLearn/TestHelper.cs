using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqLearn
{
    #region "Test Helper"
    public static class TestHelper
    {

        public static IEnumerable<int> CreateNumbersTable()
        {

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            return numbers;
        }


        public static IEnumerable<string> CreateDigitsTable()
        {

            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            return digits;
        }

        public static IEnumerable<Customer> GetCustomers()
        {
            var list = new List<Customer>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Customer { Id = i, Name = "customer" + i, OrderId = i });
            }
            list.Add(new Customer { Id = 100, Name = "customer-100", OrderId = 100 });
            return list;

        }

        public static IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            for (int i = 0; i < 20; i++)
            {
                orders.Add( new Order { Id =i   ,OrderDate= DateTime.Now,Total=i* 0.5 });

            }
            return orders;
        }


    }

    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public int OrderId { get; set; }
    }

    public class Order
    {
        public Order(int orderID, DateTime orderDate, double total)
        {
            Id = orderID;
            OrderDate = orderDate;
            Total = total;
        }

        public Order() { }

        public int Id;
        public DateTime OrderDate;
        public double Total;
    }

    #endregion
}
