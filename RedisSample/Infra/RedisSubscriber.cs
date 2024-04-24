using StackExchange.Redis;

namespace RedisSample.Infra
{
  public class RedisSubscriber : IRedisSubscriber
  {
    private readonly IConnectionMultiplexer _redis;
    private ISubscriber Subscriber;

    public RedisSubscriber(IConnectionMultiplexer redis)
    {
      _redis = redis;
      Subscriber = _redis.GetSubscriber();
    }
    public void Subscribe(string ChannelName, Action<RedisChannel, RedisValue> handler)
    {
      Subscriber.Subscribe(ChannelName, handler);
    }

    public void Unsubscribe(string channelName)
    {
      Subscriber.Unsubscribe(channelName);
    }
  }
}
