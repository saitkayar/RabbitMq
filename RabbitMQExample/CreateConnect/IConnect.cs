using RabbitMQ.Client;

namespace RabbitMQExample.CreateConnect
{
    public interface IConnect
    {
        public IConnection CheckConnection();

        public IConnection GetConnection();
    }
}