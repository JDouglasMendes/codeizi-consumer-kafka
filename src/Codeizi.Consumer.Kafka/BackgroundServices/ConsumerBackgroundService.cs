using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Codeizi.Consumer.Kafka
{
    public class ConsumerBackgroundService : BackgroundService
    {
        private readonly Consumer consumer;

        public ConsumerBackgroundService(Consumer consumer)
            => this.consumer = consumer;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            consumer.InitializeConsumers();

            while (!stoppingToken.IsCancellationRequested)
            {
                consumer.ExecuteConsumers();
                await Task.Delay(10);
            }

            await Task.CompletedTask;
        }
    }
}