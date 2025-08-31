using Confluent.Kafka;

namespace Share.KafkaWrapper;

public class ProducerWrapper : IDisposable
{
    private readonly IProducer<string, string> _producer;
    public ProducerSetting Setting { get; set; }

    public ProducerWrapper(ProducerSetting setting)
    {
        Setting = setting;
        Setting.TransactionalId = Setting.Id;
        _producer = new ProducerBuilder<string, string>(Setting).Build();
    }

    public async void SendMessage(Message<string, string>[] messages)
    {
        foreach (var message in messages)
            _producer.Produce(Setting.Topic, message);
    }

    public void InitTransaction() => _producer.InitTransactions(TimeSpan.FromSeconds(10));

    public void BeginTransaction() => _producer.BeginTransaction();

    public void CommitTransaction() => _producer.CommitTransaction();

    public void AbortTransaction() => _producer.AbortTransaction();

    public void Dispose() => _producer?.Dispose();
}
