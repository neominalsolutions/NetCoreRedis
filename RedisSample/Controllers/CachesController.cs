using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisSample.Dtos;
using RedisSample.Infra;
using StackExchange.Redis;

namespace RedisSample.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CachesController : ControllerBase
  {
    private readonly ICache cacheService;
   


    public CachesController(ICache cacheService)
    {
      this.cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> Test()
    {
      var product = new Product
      {
        Name = "P-1",
        Stock = 10,
        Price = 25
      };

      this.cacheService.Set<Product>("products", product, TimeSpan.FromMinutes(5));


      var cachedProduct = this.cacheService.Get<Product>("products");

      return Ok(cachedProduct);
    }

    [HttpGet("cache")]
    public async Task<IActionResult> GetCache()
    {
      return Ok(cacheService.Get<Product>("Products"));
    }

   

  }
}
