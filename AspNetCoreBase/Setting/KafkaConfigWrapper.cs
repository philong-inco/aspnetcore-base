using Confluent.Kafka;

namespace Main.Setting;

public class ConsumerSetting : ConsumerConfig
{
    public string? Id { get; init; }

}

public class ProducerSetting : ProducerConfig
{
    public string? Id { get; init; }
}
