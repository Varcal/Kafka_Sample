using System.Threading.Tasks;

namespace Kafka_Producer
{
    public interface IBookingProducer
    {
        Task Produce(string message);
    }
}
