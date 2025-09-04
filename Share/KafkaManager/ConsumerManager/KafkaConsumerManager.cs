using Share.KafkaWrapper;

namespace Share.KafkaManager.ConsumerManager;

public class KafkaConsumerManager : IKafkaConsumerManager
{
    private readonly Dictionary<string, ConsumerWrapper> _consumers = new();

    public void AddConsumer(ConsumerWrapper consumerWrapper)
    {
        if (!_consumers.TryGetValue(consumerWrapper.Setting.Id, out var existingSetting))
            _consumers[consumerWrapper.Setting.Id] = consumerWrapper;
    }

    public ConsumerWrapper GetConsumers(string id)
    {
        if (_consumers.TryGetValue(id, out var consumerWrapper))
            return consumerWrapper;
        return default;
    }

    public void RemoveConsumer(string id)
    {
        if (_consumers.TryGetValue(id, out var consumerWrapper))
            _consumers.Remove(id);
    }

    public bool StartConsumer(string id)
    {
        if (_consumers.TryGetValue(id, out var consumerWrapper))
        {
            consumerWrapper.Start();
            return true;
        }
        return false;
    }

    public bool StopConsumer(string id)
    {
        if (_consumers.TryGetValue(id, out var consumerWrapper))
        {
            consumerWrapper.Stop();
            return true;
        }
        return false;
    }
}
