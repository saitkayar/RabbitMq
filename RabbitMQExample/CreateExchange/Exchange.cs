using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMQExample.CreateExchange
{
    public class Exchange : IExchange
    {
        private readonly IConnection _connection;
        public Exchange(IConnection connection)
        {
            _connection = connection;
        }
        private IModel _channel;
        private IModel channel => _channel ?? (_channel = CreateOrChannel());
        public IModel CreateOrChannel()
        {
           return _connection.CreateModel();
        }

        public void DeclareExchange()
        {
            channel.ExchangeDeclare("test2", ExchangeType.Direct);
            Console.WriteLine("excahnge declared");
        }

        public void DeclareQuee()
        {
            channel.QueueDeclare("first2",false,false);
            
            Console.WriteLine("declared que" );
        }

        public void BindQue()
        {
            channel.QueueBind("first2", "test2", "first2");
            Console.WriteLine("binded");
        }

        public void publish()
        {
            datatoexchange("test", "first","first publish firsttttttttttttttttttttt");
        }

        private void datatoexchange(string exchange,string routing,object data)
        {
            var dataarray = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            channel.BasicPublish(exchange, routing, null, dataarray);

        }
    }
}
