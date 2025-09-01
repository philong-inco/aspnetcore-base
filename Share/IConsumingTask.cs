using Confluent.Kafka;

namespace Share;

public interface IConsumingTask<TKey, TValue> : IConsumingTask
{
    Task ExcuteAsync(ConsumeResult<TKey, TValue> result);
}

public interface IConsumingTask
{

}
