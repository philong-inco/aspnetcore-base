using Confluent.Kafka;
using Share.KafkaWrapper;

namespace Share.KafkaManager.ProducerManager;

public class KafkaProducerManager : IKafkaProducerManager
{
    private readonly Dictionary<string, ProducerWrapper> _producers = new();
    public void AddProducer(ProducerWrapper producerWrapper)
    {
        if (!_producers.TryGetValue(producerWrapper.Setting.Id, out var existingProducer))
            _producers[producerWrapper.Setting.Id] = producerWrapper;
    }

    public ProducerWrapper GetProducer(string id)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            return producerWrapper;
        return default;
    }

    public void RemoveProducer(string id)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            _producers.Remove(id);
    }

    public void Produce(string id, Message<string, string> messages)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            producerWrapper.SendMessage([messages]);
    }

    public void InitTransaction(string id)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            producerWrapper.InitTransaction();
    }

    public void BeginTransaction(string id)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            producerWrapper.BeginTransaction();
    }

    public void CommitTransaction(string id)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            producerWrapper.CommitTransaction();
    }

    public void AbortTransaction(string id)
    {
        if (_producers.TryGetValue(id, out var producerWrapper))
            producerWrapper.AbortTransaction();
    }
}
