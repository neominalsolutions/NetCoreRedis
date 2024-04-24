using StackExchange.Redis;

namespace RedisSample.Infra
{
  public interface ICache
  {
    void Delete(string key);
    void Set<TObject>(string key, TObject @object, TimeSpan expireTime);

    void SetHash(string hashKey, HashEntry[] @entry);

    RedisValue GetHash(string hashKey, string keyName);

    TObject Get<TObject>(string key);

    void Clear();

    Task PublishAsync<TObject>(string channelName, TObject data);

  }
}
