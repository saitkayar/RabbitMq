using RabbitMQExample.CreateConnect;
using RabbitMQExample.CreateExchange;
using System.Threading.Channels;

IConnect connect = new Connect();

var connection= connect.CheckConnection();

IExchange exchange=new Exchange(connection);


 exchange.DeclareExchange();
 exchange.DeclareQuee();
exchange.BindQue();
exchange.publish();


Console.ReadLine();





