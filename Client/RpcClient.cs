using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    public class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "118.24.150.217" };
            connection = factory.CreateConnection();//创建连接
            channel = connection.CreateModel();     //创建虚拟信道
            replyQueueName = channel.QueueDeclare().QueueName; //回调队列名称

            consumer = new EventingBasicConsumer(channel);    //创建消费者

            //设置属性
            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString(); //操作事件ID
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;   //回调队列名称


            //客户端接收消息处理
            consumer.Received += (model, ea) =>
             {
                 var body = ea.Body;
                 var response = Encoding.UTF8.GetString(body);
                 if (ea.BasicProperties.CorrelationId == correlationId)
                 {
                     respQueue.Add(response); //将收到的消息加入集合
                 }
             };
        }

        public string Call(string message)
        {
            var msgBytes = Encoding.UTF8.GetBytes(message);

            //发送消息
            channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: props, body: msgBytes);

            channel.BasicConsume(consumer: consumer,
                                queue: replyQueueName,
                           autoAck: true);

            return respQueue.Take();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
