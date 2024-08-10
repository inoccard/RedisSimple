using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RedisSimple.Models;
using StackExchange.Redis;

namespace RedisSimple.Controllers;

[ApiController]
[Route("[controller]")]
public class RedisController(IConnectionMultiplexer redis) : ControllerBase
{
    [HttpPost("save-user")]
    public async Task<IActionResult> SaveUser([FromBody] User user)
    {
        var db = redis.GetDatabase();
        
        var userExists = await db.KeyExistsAsync(user.Id);
    
        if (userExists)
            return Conflict("A user with this ID already exists.");
        
        var userJson = JsonSerializer.Serialize(user);
        
        await db.StringSetAsync(user.Id, userJson);

        return Ok("User saved successfully");
    }
    
    [HttpGet("get-user/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var db = redis.GetDatabase();
    
        // Obtém o objeto JSON do Redis
        string userJson = await db.StringGetAsync(id);

        if (string.IsNullOrEmpty(userJson))
            return NotFound();

        // Desserializa o JSON de volta para o objeto User
        var user = JsonSerializer.Deserialize<User>(userJson);

        return Ok(user);
    }

}
