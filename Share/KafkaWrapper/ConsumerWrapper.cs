using Confluent.Kafka;
using Share.KafkaSetting;

namespace Share.KafkaWrapper;

public class ConsumerWrapper
{
    public ConsumerSetting Setting { get; }
    private CancellationTokenSource _cancelTokenSource;
    private Task? _runningTask;

    public ConsumerWrapper(ConsumerSetting setting)
    {
        Setting = setting;
    }

    public bool IsRunning => _runningTask != null && !_runningTask.IsCompleted;

    public void Start()
    {
        if (IsRunning) return;

        _cancelTokenSource = new CancellationTokenSource();
        var token = _cancelTokenSource.Token;

        _runningTask = Task.Factory.StartNew(() =>
        {
            using var consumer = new ConsumerBuilder<string, string>(Setting).Build();
            consumer.Subscribe(Setting.Topic);

            try
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var consumerResult = consumer.Consume(TimeSpan.FromSeconds(3));
                        if (consumerResult != null)
                        {
                            // Thực hiện logic xử lý tin nhắn ở đây
                            Console.WriteLine($"ConsumerResult: {consumerResult.Key}, {consumerResult.Value}");

                        }
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Consume error: {ex.Error.Reason}");
                    }
                }
                
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Consumer stopped.");
            }
            finally
            {
                consumer.Close();
            }
        }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public void Stop()
    {
        if (!IsRunning) return;

        _cancelTokenSource.Cancel();
        try
        {
            _runningTask.Wait();
        }
        catch (AggregateException ex) when (ex.InnerExceptions.All(e => e is OperationCanceledException))
        {
            // Ignore cancellation exceptions
        }
        finally
        {
            _cancelTokenSource.Dispose();
            _runningTask = null;
        }
    }
}
