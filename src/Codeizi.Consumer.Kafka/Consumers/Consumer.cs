using Confluent.Kafka;
using System;
using System.Collections.Generic;

namespace Codeizi.Consumer.Kafka
{
    public class Consumer
    {
        private readonly List<ConfigurationConsumer> _configurationConsumers;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<KeyValuePair<ConfigurationConsumer, IConsumer<Ignore, string>>> _consumers;

        public Consumer(
            ConfigurationConsumer configurationConsumer,
            IServiceProvider serviceProvider)
            : this()
        {
            _configurationConsumers.Add(configurationConsumer);
            _serviceProvider = serviceProvider;
        }

        public Consumer(
            IEnumerable<ConfigurationConsumer> consumers,
            IServiceProvider serviceProvider)
            : this()
        {
            _configurationConsumers.AddRange(consumers);
            _serviceProvider = serviceProvider;
        }

        private Consumer()
        {
            _configurationConsumers = new List<ConfigurationConsumer>();
            _consumers = new List<KeyValuePair<ConfigurationConsumer, IConsumer<Ignore, string>>>();
        }

        public void InitializeConsumers()
        {
            _configurationConsumers.ForEach(x =>
            {
                var consumer = new KeyValuePair<ConfigurationConsumer, IConsumer<Ignore, string>>
                (x, GenerateTask(x));
                _consumers.Add(consumer);
            });
        }

        private IConsumer<Ignore, string> GenerateTask(ConfigurationConsumer configConsumer)
        {
            var conf = new ConsumerConfig
            {
                GroupId = configConsumer.GroupId,
                BootstrapServers = configConsumer.Server,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            var consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
            consumer.Subscribe(configConsumer.Topic);
            return consumer;
        }

        public void ExecuteConsumers()
        {
            _consumers.ForEach(consumer =>
            {
                try
                {
                    var message = consumer.Value.Consume(1000);
                    if (message != null)
                        _ = GetService(consumer.Key).Handle(message?.Message?.Value);
                }
#pragma warning disable CA1031 // Do not catch general exception types
                catch (OperationCanceledException)
                {
                    consumer.Value.Close();
                }
#pragma warning restore CA1031 // Do not catch general exception types
            });
        }

        private IConsumerKafkaTopic GetService(ConfigurationConsumer configurationConsumers)
            => _serviceProvider.GetService(configurationConsumers.TypeConsumer) as IConsumerKafkaTopic;
    }
}