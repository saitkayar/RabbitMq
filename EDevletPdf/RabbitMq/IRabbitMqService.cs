using EDevletPdf.Common.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDevletPdf.RabbitMq
{
    public interface IRabbitMqService
    {
        void CheckConnection();
        void CreateDocument();
    }
    public class RabbitMqService: IRabbitMqService
    {
     static   IConnection connection;
        static IModel _channel;
        static IModel channel => _channel ?? (_channel = GetChannel());
    

        private readonly static string url="amqp://guest:guest@localhost:5672";
        private readonly static string createQue="create_document_que";
        private readonly static string documentCreatedQue="document_created_que";
        private readonly static string documentCreatedExcange="document_created_exchange";

   

        public void CheckConnection()
        {
            if (connection == null || !connection.IsOpen) GetConnection();

            channel.ExchangeDeclare(documentCreatedExcange, "direct");
            channel.QueueDeclare(createQue, false, false, false);

            channel.QueueBind(createQue, documentCreatedExcange, createQue);

            channel.QueueDeclare(documentCreatedQue, false, false, false);
            channel.QueueBind(documentCreatedQue, documentCreatedExcange, documentCreatedQue);
            Console.WriteLine("connection is open");
        }

        private IConnection GetConnection()
        {

            var confactory = new ConnectionFactory
            {

                Uri = new Uri(url)
            };

            return confactory.CreateConnection();
        }

        public void CreateDocument()
        {
            var model = new CreateDocumentModel
            {
                UserId = 1,
                DocumentTyoe = DocumentTyoe.Pdf
            };
            WriteToQueue(createQue, model);

            var consumerEvent = new EventingBasicConsumer(channel);
            consumerEvent.Received += (sender, args) => {

                var model = JsonConvert.DeserializeObject<CreateDocumentModel>(Encoding.UTF8.GetString(args.Body.ToArray()));

                Console.WriteLine(model.Url);
            };
            channel.BasicConsume(documentCreatedQue,true,consumerEvent);
        }

        private void WriteToQueue(string queue,CreateDocumentModel model)
        {

            var message=Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            channel.BasicPublish(documentCreatedExcange,createQue,null,message);

            Console.WriteLine("message published");

        }

        private IModel GetChannel()
        {
            return connection.CreateModel();
        }
    }
}
