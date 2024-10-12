using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost"};
            using (var connection = factory.CreateConnection()) 
            using (var chanel = connection.CreateModel())
            {
                chanel.QueueDeclare("BasicTestQueue", false,false,false,null);

                string message = "Getting started with Rabbitmq2";
                var body= Encoding.UTF8.GetBytes(message);

                chanel.BasicPublish("", "BasicTestQueue",null,body);
                Console.WriteLine("Message sent {0} ..." , message); 
            }
        }
    }
}
