using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Kafka_Producer
{
    public  class BookingProducer: IBookingProducer
    {
        public async Task Produce(string message)
        {
            const string topicName = "timemanagement_booking";

            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            var clientConfig = new ClientConfig
            {
                BootstrapServers = config.BootstrapServers,
                Acks = Acks.All
            };

            var adminClient = new AdminClientBuilder(clientConfig).Build();


            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification>()
                {
                    new TopicSpecification
                    {
                        Name = topicName ,
                        NumPartitions = 2,
                        ReplicationFactor = 1
                    }
                });
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine(e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists
                    ? $"An error occured creating topic {topicName}: {e.Results[0].Error.Reason}"
                    : "Topic already exists");
            }
            

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        var delivery = await producer.ProduceAsync( topicName, new Message<Null, string>() { Value = message });
                        Console.WriteLine($"Entregue '{ delivery.Value }' para '{ delivery.TopicPartitionOffset }'");
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
