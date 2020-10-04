using System;

namespace Codeizi.Consumer.Kafka
{
    public class ConfigurationConsumer
    {
        public ConfigurationConsumer(string server)
            => Server = server;

        public string Server { get; }
        internal string GroupId { get; set; }
        internal string Topic { get; set; }
        internal Type TypeConsumer { get; set; }
    }
}