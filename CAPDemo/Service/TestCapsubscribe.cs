using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;

namespace CAPDemo.Service
{
  

    public interface ISubscriberService
    {
        void CheckReceivedMessage(dynamic model);
    }


    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("capdemo.values.getmethodevent")]
        public void CheckReceivedMessage(dynamic model)
        {
            Console.WriteLine($"[capdemo.values.getmethodevent] message received: Id:{model.Id}  Time:{model.Time}  Message:{model.Message} ");
        }
    }
}
