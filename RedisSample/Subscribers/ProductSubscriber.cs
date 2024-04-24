using RedisSample.Dtos;
using RedisSample.Infra;
using System.Text.Json;

namespace RedisSample.Subscribers
{
  // Product Channel Subscriber Implementasyonu
  public class ProductSubscriber : BackgroundService
  {
    private readonly IRedisSubscriber redisSubscriber;
    private readonly ICache cache;

    public ProductSubscriber(IRedisSubscriber redisSubscriber, ICache cache)
    {
      this.redisSubscriber = redisSubscriber;
      this.cache = cache;
    }
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        // Her gönderimde arka planda subscribe olduk.
        // Örnek olarak Cache bozulduğunda Cahce Temizle Yeniden Düzenle
        // Ürün Kaydı yapıldığında ürünün cache temizleyecek kod yazılır.
        // Products Key Cache Her değiştiğinde Cache temizle yeniden set et.
        redisSubscriber.Subscribe("Products", (channel, message) =>
        {

          var cacheValue = JsonSerializer.Deserialize<Product>(message);

          this.cache.Delete("Products");
          this.cache.Set<Product>("Products", cacheValue, TimeSpan.FromMinutes(5));

          Console.WriteLine($"Received message: {message} from channel: {channel}");

        });


      if (stoppingToken.IsCancellationRequested)
      {
        redisSubscriber.Unsubscribe("Product-Channel");
      }
    }
  }
}
