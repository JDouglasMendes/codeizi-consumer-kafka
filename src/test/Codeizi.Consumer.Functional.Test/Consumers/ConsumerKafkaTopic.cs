using Codeizi.Consumer.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Codeizi.Consumer.Functional.Test.Consumers
{
    public class ConsumerKafkaTopic : IConsumerKafkaTopic
    {
        private readonly InMemoryDatabase database;
        private readonly ILogger<ConsumerKafkaTopic> _logger;
        public ConsumerKafkaTopic(
            InMemoryDatabase database,
            ILogger<ConsumerKafkaTopic> logger)
        {
            this.database = database;
            _logger = logger;
        }

        public Task Handle(string message)
        {
            _logger.LogInformation(message);
            var test = JsonConvert.DeserializeObject<TypeConsumer>(message);
            database.AddMessage(test);
            return Task.CompletedTask;
        }
    }
}