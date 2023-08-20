using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.CreateConnect
{
    public class Connect : IConnect
    {


        private IConnection connection;
        private bool isConnectionopen;
        private bool _isConnectionopen;

        private IModel _channel;
        private IModel channel => _channel ?? (_channel = CreateOrChannel());
        public IModel CreateOrChannel()
        {
            return connection.CreateModel();
        }


        public IConnection CheckConnection()
        {
            if (!isConnectionopen || connection == null)
            {
                connection = GetConnection();
            }
            else
                connection.Close();

            isConnectionopen = connection.IsOpen;
            Console.WriteLine(isConnectionopen);

            return connection;
        }

        public IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672", UriKind.RelativeOrAbsolute)
            };

            return factory.CreateConnection();
        }

        public void Consumer()
        {
            var eve = new EventingBasicConsumer(channel);
            eve.Received+=(ch,e) =>  {

                var arr = e.Body.ToArray();
                var str = Encoding.UTF8.GetString(arr);
                Console.WriteLine("consumer data "+str);
            };
           

            channel.BasicConsume("first2", true, eve);
            Console.ReadLine();

            Console.WriteLine("listening done");


        }
     
    }


}
