using System;

namespace Codeizi.Consumer.Kafka
{
    public class NotConfiguredException : Exception
    {
        public NotConfiguredException(string message) : base(message)
        {
        }

        public NotConfiguredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotConfiguredException()
        {
        }
    }
}