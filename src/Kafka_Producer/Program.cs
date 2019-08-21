using System;
using System.Threading.Tasks;

namespace Kafka_Producer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var bookingProducer = new BookingProducer();
            await bookingProducer.Produce("Minha primeira mensagem");
        }
    }
}
