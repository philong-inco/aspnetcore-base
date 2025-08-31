using Confluent.Kafka;

namespace Share.KafkaWrapper;

public class ProducerWrapper //: IDisposable
{
    private  IProducer<string, string> _producer;
    public ProducerSetting Setting { get; set; }

    public ProducerWrapper(ProducerSetting setting)
    {
        Setting = setting;
        Setting.TransactionalId = Setting.Id;
        //_producer = new ProducerBuilder<string, string>(Setting).Build();
    }

    public async void SendMessage(Message<string, string>[] messages)
    {

        //var producerConfig = new ProducerSetting()
        //{
        //    BootstrapServers = "localhost:19092,localhost:29092,localhost:39092",
        //};

        //var producer = new ProducerBuilder<string, string>(producerConfig).Build();
        _producer = new ProducerBuilder<string, string>(Setting).Build();

        //var producertest = new ProducerBuilder<string, string>(Setting).Build();

        foreach (var message in messages)
            _producer.Produce(Setting.Topic, message);
    }

    public void InitTransaction() => _producer.InitTransactions(TimeSpan.FromSeconds(10));

    public void BeginTransaction() => _producer.BeginTransaction();

    public void CommitTransaction() => _producer.CommitTransaction();

    public void AbortTransaction() => _producer.AbortTransaction();

    //public void Dispose() => _producer?.Dispose();
}
