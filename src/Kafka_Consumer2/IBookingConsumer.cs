using System;
using System.Collections.Generic;
using System.Text;

namespace Kafka_Consumer
{
    public interface IBookingConsumer
    {
        void Listen(Action<string> message);
    }
}
