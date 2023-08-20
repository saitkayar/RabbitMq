using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQExample.CreateExchange
{
    public interface IExchange
    {
        IModel CreateOrChannel ();
        void DeclareExchange ();
        void DeclareQuee ();
        void BindQue ();
        void publish ();
    }
}
