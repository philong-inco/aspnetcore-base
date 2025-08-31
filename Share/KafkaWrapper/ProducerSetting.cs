using Confluent.Kafka;

namespace Share.KafkaWrapper;

public class ProducerSetting : ProducerConfig
{
    public string Id { get; set; }
    public string Topic { get; set; }
}
