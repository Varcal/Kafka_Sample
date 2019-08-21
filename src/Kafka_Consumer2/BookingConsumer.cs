using System;
using System.Threading;
using Confluent.Kafka;
using Kafka_Consumer;

namespace Kafka_Consumer2
{
    public class BookingConsumer: IBookingConsumer
    {
        public void Listen(Action<string> message)
        {
            var config = new ConsumerConfig
            {
                GroupId = "booking_consumer",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("timemanagement_booking");
                var cancellationTokenSource = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cancellationTokenSource.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumer2 = consumer.Consume(cancellationTokenSource.Token);
                            Console.WriteLine($"Mensagem consumida '{consumer2.Value}' em: '{consumer2.TopicPartitionOffset}'");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Ocorreu erro: {e.Error.Reason}");
                            throw;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
        }
    }
}
