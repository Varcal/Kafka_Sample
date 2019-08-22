using System;

namespace Kafka_Consumer
{
    public interface IBookingConsumer
    {
        void Listen(Action<string> message);
    }
}
