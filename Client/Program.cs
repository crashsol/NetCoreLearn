using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "118.24.150.217"
            };
            #region 发送接受模式
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        //申明一个hello队列
            //        channel.QueueDeclare(queue: "hello",
            //                          durable: false,
            //                      exclusive: false,
            //                          autoDelete: false,
            //                          arguments: null);

            //        // 创建一个消费者
            //        var customer = new EventingBasicConsumer(channel);
            //        ///消费者接受事件定义
            //        customer.Received += (sender, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine(" Client Received {0}", message);
            //        };
            //        //消费者与队列绑定
            //        channel.BasicConsume(queue: "hello",
            //                  autoAck: true,
            //                  consumer: customer);

            //        Console.WriteLine(" Press [enter] to exit Client.");
            //        Console.ReadLine();
            //    }

            //}
            #endregion

            #region Work Queues 多消费者模式

            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        //申明一个 task_queue 队列
            //        channel.QueueDeclare(queue: "task_queue",
            //                          durable: true,  //持续的队列，不会被销毁
            //                          exclusive: false,
            //                          autoDelete: false,
            //                          arguments: null);

            //        Console.WriteLine("Waiting for messages.");

            //        //设置Qos 每次只取一条数据
            //        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            //        // 创建一个消费者
            //        var customer = new EventingBasicConsumer(channel);
            //        ///消费者接受事件定义
            //        customer.Received += (sender, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine(" Client Received {0}", message);


            //            //模拟耗时操作
            //            int dots = message.Split('.').Length - 1;
            //            Thread.Sleep(dots * 1000);

            //            Console.WriteLine(" [x] Done");

            //            //发送回执
            //            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            //        };
            //        //消费者与队列绑定
            //        channel.BasicConsume(queue: "task_queue",
            //                  autoAck: true,
            //                  consumer: customer);

            //        Console.WriteLine(" Press [enter] to exit Client.");
            //        Console.ReadLine();
            //    }

            //}


            #endregion

            #region  Publish/Subscribe 

            ////  fanout  => broadcast 广播，只要队列与该交换器进行了注册，都会收到消息
            ////  direct  => 定向路由 ，如果订阅的队列 routingKey 一致将会收到消息
            ////  topic   => 根据routingkey 进行路由匹配,与交换机路由处理类似，全匹配和部分匹配，routingkey 用 '.'分割，支持 * 模糊匹配一个单词，# 多个单词
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel())
            //    {
            //        //申明一个交换器
            //        channel.ExchangeDeclare(exchange: "logs", type: "fanout");

            //        //声明一个匿名的队列名称
            //        var queueName = channel.QueueDeclare().QueueName;

            //        channel.QueueBind(queue: queueName, //绑定的队列名称
            //                          exchange: "logs", //绑定的交换器名称
            //                          routingKey: "");
            //        //创建消费者
            //        var consumer = new EventingBasicConsumer(channel);
            //        consumer.Received += (model, ea) =>
            //        {
            //            var body = ea.Body;
            //            var message = Encoding.UTF8.GetString(body);
            //            Console.WriteLine($"客户端收到消息 {message}"); 
            //        };
            //        //将消费者与channel绑定，并自动发送ACK确认
            //        channel.BasicConsume(queue: queueName,
            //                              autoAck: true,
            //                              consumer: consumer);

            //        Console.WriteLine(" Press [enter] to exit.");
            //        Console.ReadLine();
            //    }

            //}

            #endregion


            #region RPC  Remote Procedure Call 远程服务调用


            var rpcClient = new RpcClient();

            Console.WriteLine(" [x] Requesting fib(30)");
            var response = rpcClient.Call("30");

            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();

            #endregion
            Console.ReadLine();


        }

    }
}
