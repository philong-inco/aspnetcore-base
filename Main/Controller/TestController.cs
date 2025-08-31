using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Share.KafkaManager.ProducerManager;

namespace Main.Controller;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IKafkaProducerManager _producer;

    public TestController(IKafkaProducerManager producer)
    {
        _producer = producer;
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

    [HttpGet("1")] 
    public async Task<IActionResult> Test()
    {
        return Ok("vcl");
    }
}
