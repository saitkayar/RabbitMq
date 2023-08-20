using RabbitMQ.Client;

namespace Consumer.CreateConnect
{
    public interface IConnect
    {
        public IConnection CheckConnection();

        public IConnection GetConnection();
        public void Consumer();
    }
}