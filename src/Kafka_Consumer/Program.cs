using System;

namespace Kafka_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookingConsumer = new BookingConsumer();
            bookingConsumer.Listen(Console.WriteLine);
        }
    }
}
