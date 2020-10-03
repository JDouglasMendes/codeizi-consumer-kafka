using Codeizi.Consumer.Functional.Test.Consumers;
using Codeizi.DI.Anotations;
using System.Collections.Generic;

namespace Codeizi.Consumer.Functional.Test
{
    [InjectableSingleton]
    public class InMemoryDatabase
    {
        public List<TypeConsumer> Message { get; }

        public InMemoryDatabase()
            => Message = new List<TypeConsumer>();

        public void AddMessage(TypeConsumer typeProducer)
            => Message.Add(typeProducer);
    }
}