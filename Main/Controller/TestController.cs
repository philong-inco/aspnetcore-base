using Confluent.Kafka;
using Main.Common;
using Microsoft.AspNetCore.Mvc;
using Share.KafkaManager.ConsumerManager;
using Share.KafkaManager.ProducerManager;

namespace Main.Controller;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IKafkaProducerManager _producer;
    private readonly IKafkaConsumerManager _consumer;

    public TestController(IKafkaProducerManager producer, IKafkaConsumerManager consumer)
    {
        _producer = producer;
        _consumer = consumer;
    }

    [HttpGet("send-massage")]
    public async Task<IActionResult> SendMessage(string message)
    {
        try
        {
            
            var messages = new Message<string, string>();
            messages.Key = message;
            messages.Value = message;
            var producer = _producer.GetProducer("ProducerTest");
            producer.SendMessage(new Message<string, string>[] { messages });
            return Ok("Success");

        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet("disable-comsumer")]
    public async Task<IActionResult> DisableConsumer()
    {
        try
        {
            _consumer.StopConsumer(Constant.ConsumerTestId);
            return Ok("Success");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("enable-comsumer")]
    public async Task<IActionResult> EnableConsumer()
    {
        try
        {
            _consumer.StartConsumer(Constant.ConsumerTestId);
            return Ok("Success");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
