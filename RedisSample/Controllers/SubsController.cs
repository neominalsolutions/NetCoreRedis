using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisSample.Infra;

namespace RedisSample.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SubsController : ControllerBase
  {
    private IRedisSubscriber subs;

    public SubsController(IRedisSubscriber subs)
    {
      this.subs = subs;

      
    }


  }
}
