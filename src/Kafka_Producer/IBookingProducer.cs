using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kafka_Producer
{
    public interface IBookingProducer
    {
        Task Produce(string message);
    }
}
