using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Codeizi.Consumer.Kafka
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConsumerKafa(
            this IServiceCollection services,
            string server)
        {
            if (string.IsNullOrEmpty(server))
                throw new ArgumentNullException(nameof(server));

            var types = GetTypesConsumer();

            if (types == null || !types.Any())
                throw new NotConfiguredException("No topic has been configured to consume");

            var configurationsConsumers = new List<ConfigurationConsumer>();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<ConsumerKafkaAttribute>();

                var configurationConsumer = new ConfigurationConsumer(server)
                {
                    GroupId = attribute.GroupId,
                    TypeConsumer = type,
                    Topic = attribute.Topic
                };

                services.AddSingleton(configurationConsumer.TypeConsumer);
                configurationsConsumers.Add(configurationConsumer);
            }

            services.AddSingleton(sp =>
            {
                return new Consumer(configurationsConsumers, sp);
            });
            services.AddHostedService<ConsumerBackgroundService>();
            return services;
        }

        private static List<Type> GetTypesConsumer()
        {
            var types = new List<Type>();

            foreach (var assembly in AppDomain.
                                            CurrentDomain.
                                            GetAssemblies().
                                            Reverse())
            {
                var typeOfAssembly = assembly.
                    GetTypes().
                    Where(x => x.GetCustomAttribute<ConsumerKafkaAttribute>() != null).
                    ToList();

                if (typeOfAssembly != null && typeOfAssembly.Any())
                    types.AddRange(typeOfAssembly);
            }
            return types;
        }
    }
}