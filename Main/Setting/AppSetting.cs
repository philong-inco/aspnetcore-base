using Confluent.Kafka;
using Share.KafkaManager.ConsumerManager;
using Share.KafkaManager.ProducerManager;
using Share.KafkaWrapper;

namespace Main.Setting;

/// <summary>
/// Lưu trữ thông tin cấu hình ứng dụng
/// </summary>
public class AppSetting
{
    public string DBDefaultTableSchema { get; init; }
    public string DB1ConnectionString { get; init; }
    public string DB2ConnectionString { get; init; }
    public int InstanceNumber { get; init; }
    public int TotalInstances { get; init; }
    public ConsumerSetting[] ConsumerSettings { get; set; } 
    public ProducerSetting[] ProducerSettings { get; set; }

    public static AppSetting MapValue(IConfiguration configuration)
    {
        // Service information map
        var serviceInformation = configuration.GetSection("ServiceInformation");
        var instanceNumber = serviceInformation.GetValue<int>("InstanceNumber");
        var totalInstances = serviceInformation.GetValue<int>("TotalInstances");

        // Consumer settings map
        var consumerSettings = configuration.GetSection("ConsumerSettings").Get<ConsumerSetting[]>();
        foreach (var consumerSetting in consumerSettings)
            consumerSetting.GroupId += $"-{instanceNumber}@{totalInstances}";

        // Producer settings map
        var producerSettings = configuration.GetSection("ProducerSettings").Get<ProducerSetting[]>();

        var appSetting = new AppSetting
        {
            DBDefaultTableSchema = configuration.GetValue<string>(nameof(DBDefaultTableSchema)),
            DB1ConnectionString = configuration.GetValue<string>(nameof(DB1ConnectionString)),
            DB2ConnectionString = configuration.GetValue<string>(nameof(DB2ConnectionString)),
            InstanceNumber = instanceNumber,
            TotalInstances = totalInstances,
            ConsumerSettings = consumerSettings,
            ProducerSettings = producerSettings
        };

        return appSetting;
    }

    public ConsumerSetting GetConsumerSetting(string id)
    => ConsumerSettings.FirstOrDefault(consumerConfig => consumerConfig.Id.Equals(id));

    public ProducerSetting GetProducerSetting(string id)
    => ProducerSettings.FirstOrDefault(producerConfig => producerConfig.Id.Equals(id));
}
