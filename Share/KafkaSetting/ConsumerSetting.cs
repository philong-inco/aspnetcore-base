using Confluent.Kafka;

namespace Share.KafkaSetting;

public class ConsumerSetting : ConsumerConfig
{
    public string Id { get; set; }
    public string Topic { get; set; }
}
