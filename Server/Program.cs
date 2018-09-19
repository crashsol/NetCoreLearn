using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置RabbitMQ服务地址
            var factory = new ConnectionFactory()
            {
                HostName = "118.24.150.217"
            };
            #region 发送接受模式
            //创建连接
            //using (var connection = factory.CreateConnection())
            //{
            //    //创建信道
            //    using (var channel = connection.CreateModel())
            //    {
            //        //声明一个 hello 队列
            //        channel.QueueDeclare(queue: "hello",
            //                     durable: false,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null); 

            //        //发送消息
            //        string message = "1111111111111111111111 World!";
            //        var body = Encoding.UTF8.GetBytes(message);

            //        channel.BasicPublish(exchange: "",
            //                             routingKey: "hello",  //队列名称
            //                             basicProperties: null,
            //                             body: body);
            //        Console.WriteLine(" [x] Sent {0}", message);
            //    }

            //}
            #endregion

            #region Work Queues 多消费者模式

            //创建连接
            //using (var connection = factory.CreateConnection())
            //{
            //    //创建信道
            //    using (var channel = connection.CreateModel())
            //    {


            //        //声明一个 taskQue 队列
            //        channel.QueueDeclare(queue: "task_queue",
            //                     durable: true,  //队列不会被销毁
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);

            //        //发送消息
            //        string message = GetMessage(args);
            //        var body = Encoding.UTF8.GetBytes(message);

            //        //设置信道参数，持久化, Server关闭后消息也不丢失
            //        var properties = channel.CreateBasicProperties();
            //        properties.Persistent = true;

            //        channel.BasicPublish(exchange: "",
            //                             routingKey: "task_queue",  //队列名称
            //                             basicProperties: properties, //设置信道参数
            //                             body: body);
            //        Console.WriteLine(" [x] Sent {0}", message);
            //    }


            //}

            #endregion

            #region Publish/Subscribe
            //using (var connection = factory.CreateConnection())
            //{
            //    using (var channel = connection.CreateModel() )
            //    {
            //        //申明一个交换器 type类型 direct, topic(常用),  fanout(常用)
            //        channel.ExchangeDeclare(exchange: "logs", type:"fanout");

            //        Console.WriteLine("请输入要发送的内容,Exit退出发送！");
            //        while (true)
            //        {

            //            var message = Console.ReadLine();
            //            if(message =="Exit")
            //            {
            //                break;
            //            }
            //            var body = Encoding.UTF8.GetBytes(message);

            //            //发布消息与交换器logs进行绑定
            //            channel.BasicPublish(exchange: "logs",
            //                                 routingKey: "",
            //                                 basicProperties: null,
            //                                 body: body);
            //            Console.WriteLine($"发送消息 {message} 成功");

            //        }                  
            //    }
            //}

            #endregion

            #region RPC Call

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rpc_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "rpc_queue",
                                     autoAck: false,
                                     consumer: consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

                consumer.Received += (model, ea) =>
                {
                    string response = null;

                    var body = ea.Body;
                    //获取Client props 属性
                    var props = ea.BasicProperties;
                    //创建Response Props
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId; //设定回复唯一Id与Client一致

                    try
                    {
                        //处理客户端消息请求内容
                        var message = Encoding.UTF8.GetString(body); 
                        int n = int.Parse(message);
                        Console.WriteLine(" [.] fib({0})", message);
                        response = fib(n).ToString();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" [.] " + e.Message);
                        response = "";
                    }
                    finally
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "", 
                                             routingKey: props.ReplyTo,  //向Client回调队列routingkey
                                             basicProperties: replyProps, //repsonse 属性，用户验证Client CorrelationId
                                             body: responseBytes);

                        channel.BasicAck(deliveryTag: ea.DeliveryTag,multiple: false);
                    }
                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

            #endregion



        }


        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }


        private static int fib(int n)
        {
            if (n == 0 || n == 1)
            {
                return n;
            }

            return fib(n - 1) + fib(n - 2);
        }
    }
}
