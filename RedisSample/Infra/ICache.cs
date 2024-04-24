namespace RedisSample.Infra
{
  public interface ICache
  {
    void Delete(string key);
    void Set<TObject>(string key, TObject @object, TimeSpan expireTime);

    TObject Get<TObject>(string key);

    void Clear();

    Task PublishAsync<TObject>(string channelName, TObject data);

  }
}
