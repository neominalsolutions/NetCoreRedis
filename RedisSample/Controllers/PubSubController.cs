using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisSample.Dtos;
using RedisSample.Infra;
using StackExchange.Redis;

namespace RedisSample.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PubSubController : ControllerBase
  {
    private readonly ICache cache;


    public PubSubController(ICache cache)
    {
      this.cache = cache;
    }
    
    [HttpPost]
    public async Task<IActionResult> PulishMessage()
    {
      var random = new Random();

      var message = new Product();
      message.Name = $"Product {Guid.NewGuid().ToString()}";
      message.Price = random.Next(0,100);
      message.Stock = random.Next(0,500);

      await this.cache.PublishAsync<Product>("products", message);

      return Ok();

    }
  }
}
