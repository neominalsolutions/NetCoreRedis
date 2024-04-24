using Microsoft.AspNetCore.DataProtection.KeyManagement;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RedisSample.Infra
{
  public class RedisCacheRepository : ICache
  {

    private readonly IConnectionMultiplexer _redisConnection;
    private readonly IDatabase _database;

    public RedisCacheRepository(IConnectionMultiplexer redisConnection)
    {
      _redisConnection = redisConnection;
      _database = _redisConnection.GetDatabase();
    }
    public void Clear()
    {
      _database.Execute("FLUSHDB"); // tüm kayıtları sil
    }

    public void Delete(string key)
    {
      _database.KeyDelete(key);
    }


    public TObject Get<TObject>(string key)
    {

      string serializedObject = _database.StringGet(key);

      if (serializedObject != null)
      {
        return JsonSerializer.Deserialize<TObject>(serializedObject);
      }
        return default; 
    }

    public RedisValue GetHash(string hashKey, string keyName)
    {
      return _database.HashGet(hashKey, keyName);
    }

    public async Task PublishAsync<TObject>(string channelName, TObject data)
    {
      var jsonString = JsonSerializer.Serialize<TObject>(data);
      await _database.PublishAsync(channelName, jsonString);
    }

    public void Set<TObject>(string key, TObject @object, TimeSpan expireTime)
    {
      if(!_database.KeyExists(key))
      {
        string serializedJson = System.Text.Json.JsonSerializer.Serialize<TObject>(@object);
        _database.StringSet(key, serializedJson, expireTime);
      }

    }

    public void SetHash(string hashKey, HashEntry[] entry)
    {
      _database.HashSet(hashKey,entry);
    }
  }
}
