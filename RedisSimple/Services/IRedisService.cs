namespace RedisSimple.Services;

using Models;
public interface IRedisService
{
    Task<bool> KeyExistsAsync(string id);
    Task<bool> StringSetAsync(User user);
    Task<string> StringGetAsync(string id);
}