using Microsoft.Extensions.DependencyInjection;

namespace Codeizi.Consumer.Kafka
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConsumerKafa(
            this IServiceCollection services,
            ConfigurationConsumer configurationConsumer)
        {
            services.AddSingleton(configurationConsumer.TypeConsumer);
            services.AddSingleton(sp =>
            {
                return new Consumer(configurationConsumer, sp);
            });
            services.AddHostedService<ConsumerBackgroundService>();
            return services;
        }
    }
}