using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Producer
{
    public class Reciever
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var chanel = connection.CreateModel())
            {
                chanel.QueueDeclare("BasicTestQueue", false, false, false, null);

                var consumer = new EventingBasicConsumer(chanel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Recieved {0}",message);
                };

                chanel.BasicConsume("BasicTestQueue", true, consumer);
                Console.WriteLine("Consuming done");
            }
        }
    }
}
