using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQExample.CreateConnect
{
    public class Connect : IConnect
    {


        private IConnection connection;
        private bool isConnectionopen;
        private bool _isConnectionopen;




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
    }


}
