using StackExchange.Redis;

namespace RedisSample.Infra
{
  public interface IRedisSubscriber
  {
    void Subscribe(string ChannelName,Action<RedisChannel, RedisValue> handler);
    void Unsubscribe(string channelName);
  }
}
