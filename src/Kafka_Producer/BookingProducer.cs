using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Kafka_Producer
{
    public  class BookingProducer: IBookingProducer
    {
        public async Task Produce(string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var delivery = await producer.ProduceAsync("timemanagement_booking", new Message<Null, string>() { Value = message });
                        Console.WriteLine($"Entregue '{delivery.Value}' para '{delivery.TopicPartitionOffset}'");
                    }

                    producer.Flush(TimeSpan.FromSeconds(10));
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Entrega falhou: {e.Error.Reason}");
                }
            }
        }
    }
}
