using Confluent.Kafka;
using Share.KafkaSetting;

namespace Share.KafkaWrapper;

public class ProducerWrapper : IDisposable
{
    private IProducer<string, string> _producer;
    private readonly IProducer<string, string> _nonTransactionalProducer;
    private readonly IProducer<string, string> _transactionProducer;
    public ProducerSetting Setting { get; set; }

    public ProducerWrapper(ProducerSetting setting)
    {
        Setting = setting;
        _nonTransactionalProducer = new ProducerBuilder<string, string>(Setting).Build();
        Setting.TransactionalId = Setting.Id;
        _transactionProducer = new ProducerBuilder<string, string>(Setting).Build();
        _producer = _nonTransactionalProducer;
    }

    public async void SendMessage(Message<string, string>[] messages)
    {
        foreach (var message in messages)
            _producer.Produce(Setting.Topic, message);
    }
    public void OnModeTransactional()
    => _producer = _transactionProducer;

    public void OffModeTransactional()
    => _producer = _nonTransactionalProducer;

    public void InitTransaction()
    {
        OnModeTransactional();
        _producer.InitTransactions(TimeSpan.FromSeconds(5));
    }

    public void BeginTransaction() => _producer.BeginTransaction();

    public void CommitTransaction()
    {
        OffModeTransactional();
        _producer.CommitTransaction();
    }

    public void AbortTransaction()
    {
        OffModeTransactional();
        _producer.AbortTransaction();
    }

    public void Dispose() => _producer?.Dispose();
}
