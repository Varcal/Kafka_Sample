using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Kafka_Producer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var bookingProducer = new BookingProducer();
            await bookingProducer.Produce("Minha primeira mensagem");

            Console.ReadKey();
        }
    }
}
