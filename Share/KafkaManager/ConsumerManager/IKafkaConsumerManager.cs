using Share.KafkaWrapper;

namespace Share.KafkaManager.ConsumerManager;

public interface IKafkaConsumerManager
{
    void AddConsumer(ConsumerWrapper consumerWrapper);
    void RemoveConsumer(string id);
    ConsumerWrapper GetConsumers(string id);
    bool StartConsumer(string id);
    bool StopConsumer(string id);
}
