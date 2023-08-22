using EDevletPdf.RabbitMq;

IRabbitMqService service = new RabbitMqService();

service.CheckConnection();

service.CreateDocument();