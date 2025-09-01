using Confluent.Kafka;

namespace Share.KafkaSetting;

public class ProducerSetting : ProducerConfig
{
    public string Id { get; set; }
    public string Topic { get; set; }
}
