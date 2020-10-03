using System;

namespace Codeizi.Consumer.Kafka
{
    public class ConfigurationConsumer
    {
        public ConfigurationConsumer(
            string groupId,
            string server,
            string topic,
            Type typeConsumer)
        {
            GroupId = groupId;
            Server = server;
            Topic = topic;
            TypeConsumer = typeConsumer;
        }

        public string GroupId { get; }
        public string Server { get; }
        public string Topic { get; }
        public Type TypeConsumer { get; }
    }
}