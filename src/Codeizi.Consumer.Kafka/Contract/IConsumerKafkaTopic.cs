using System.Threading.Tasks;

namespace Codeizi.Consumer.Kafka
{
    public interface IConsumerKafkaTopic
    {
        Task Handle(string message);
    }
}