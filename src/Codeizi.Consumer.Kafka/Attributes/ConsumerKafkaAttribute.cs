using System;

namespace Codeizi.Consumer.Kafka
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ConsumerKafkaAttribute : Attribute
    {
        public string Topic { get; }
        public string GroupId { get; }

        public ConsumerKafkaAttribute(
            string groupId,
            string topic)
            => (GroupId, Topic) = (groupId ,topic);
    }
}