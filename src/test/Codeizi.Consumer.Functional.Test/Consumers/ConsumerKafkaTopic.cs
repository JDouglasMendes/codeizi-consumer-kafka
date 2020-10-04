using Codeizi.Consumer.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Codeizi.Consumer.Functional.Test.Consumers
{
    [ConsumerKafka(
        nameof(TEST_CONSUMER_GROUP),
        nameof(Topic_Test))]
    public class ConsumerKafkaTopic : IConsumerKafkaTopic
    {
        public const string Topic_Test = "Topic_Test";
        public const string TEST_CONSUMER_GROUP = "TEST_CONSUMER_GROUP";

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
            var myType = JsonConvert.DeserializeObject<TypeConsumer>(message);
            database.AddMessage(myType);
            return Task.CompletedTask;
        }
    }
}