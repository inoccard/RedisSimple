using System.Text.Json;
using RedisSimple.Models;
using StackExchange.Redis;

namespace RedisSimple.Services;

public class RedisService(IConnectionMultiplexer redis) : IRedisService
{
    private readonly IDatabase _connection = redis.GetDatabase();
    
    public async Task<bool> KeyExistsAsync(string id) 
        => await _connection.KeyExistsAsync(id);

    public async Task<bool> StringSetAsync(User user)
    {
        var userJson = JsonSerializer.Serialize(user);
        return await _connection.StringSetAsync(user.Id, userJson);
    }

    public async Task<string> StringGetAsync(string id) 
        => await _connection.StringGetAsync(id);
}