

# Codeizi.Consumer.Kafka

![GitHub](https://img.shields.io/badge/Codeizi-Framework-blueviolet)
![GitHub](https://img.shields.io/github/license/JDouglasMendes/codeizi-consumer-kafka)

### Instalação

`Install-Package Codeizi.Consumer.Kafka`

### Configuração

Na classe `Startup` no método `ConfigureServices` adicionar o código:

````
    services.AddConsumerKafa("ServerName:Port");
````

### Utilização

A classe que receberá as mensagens deverá implementar a interface `IConsumerKafkaTopic` e ter o atributo `ConsumerKafka` configurado. Veja exemplo:


````
    [ConsumerKafka(
        nameof(TEST_CONSUMER_GROUP),
        nameof(Topic_Test))]
    public class ConsumerKafkaTopic : IConsumerKafkaTopic
    {
        ....
        public Task Handle(string message)
        {           
            var myType = JsonConvert.DeserializeObject<TypeConsumer(message)         
            return Task.CompletedTask;
        }
    }
````

Perceba que o método `Handle` recebe uma string em formato `JSON`.