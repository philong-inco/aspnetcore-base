using Main.Setting;
using Share.KafkaManager.ConsumerManager;
using Share.KafkaManager.ProducerManager;
using Share.KafkaWrapper;

namespace Main.Extensions;

public static class HostExtensions
{
    public static IHost InitKafkaConsumer (this IHost host, AppSetting appSetting)
    {
        var serviceProvider = host.Services;
        var kafkaConsumerManager = (IKafkaConsumerManager)serviceProvider.GetRequiredService(typeof(IKafkaConsumerManager));

        var consumerSettings = appSetting.ConsumerSettings;
        foreach (var consumer in consumerSettings)
        {
            var consumerWrapper = new ConsumerWrapper(consumer);
            kafkaConsumerManager.AddConsumer(consumerWrapper);
        }

        return host;
    }
    
    public static IHost InitKafkaProducer(this IHost host, AppSetting appSetting)
    {
        var serviceProvider = host.Services;
        var kafkaProducerManager = (IKafkaProducerManager)serviceProvider.GetRequiredService(typeof(IKafkaProducerManager));

        var producerSettings = appSetting.ProducerSettings;
        foreach (var producer in producerSettings)
        {
            var consumerWrapper = new ProducerWrapper(producer);
            kafkaProducerManager.AddProducer(consumerWrapper);
        }

        return host;
    }

    public static IHost RunConsumers(this IHost host, Action<IKafkaConsumerManager> manager)
    {
        var serviceProvider = host.Services;
        var kafkaConsumerManager = (IKafkaConsumerManager)serviceProvider.GetRequiredService(typeof(IKafkaConsumerManager));
        manager(kafkaConsumerManager);

        return host;
    }
}
