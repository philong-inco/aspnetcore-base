using Microsoft.Extensions.DependencyInjection;
using Share.KafkaManager.ConsumerManager;
using Share.KafkaManager.ProducerManager;

namespace Share.Extentsions;

public static class ServiceCollectionExtensions 
{
    public static IServiceCollection AddKafkaManager(this IServiceCollection services)
    {
        services.AddSingleton<IKafkaConsumerManager, KafkaConsumerManager>();
        services.AddSingleton<IKafkaProducerManager, KafkaProducerManager>();
        return services;    
    }
}
