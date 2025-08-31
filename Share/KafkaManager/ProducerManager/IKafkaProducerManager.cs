using Confluent.Kafka;
using Share.KafkaWrapper;

namespace Share.KafkaManager.ProducerManager;

public interface IKafkaProducerManager
{
    void AddProducer(ProducerWrapper producerWrapper);
    void RemoveProducer(string id);
    ProducerWrapper GetProducer(string id);
    void Produce(string id, Message<string, string> messages);
    void InitTransaction(string id);
    void BeginTransaction(string id);
    void CommitTransaction(string id);
    void AbortTransaction(string id);
}
