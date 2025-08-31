using Main.Setting;

namespace Main.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, AppSetting appSetting)
    {
        
        return services;
    }

    public static IServiceCollection AddKafkaProducer(this IServiceCollection services, AppSetting appSetting)
    {
        // Register Kafka producer related services here
        return services;
    }

    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        // Register scoped services here
        return services;
    }

    public static IServiceCollection AddSingletonService(this IServiceCollection services)
    {
        return services;
    } 
}
