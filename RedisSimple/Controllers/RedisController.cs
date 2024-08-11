using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RedisSimple.Models;
using RedisSimple.Services;

namespace RedisSimple.Controllers;

[ApiController]
[Route("[controller]")]
public class RedisController(IRedisService redisService) : ControllerBase
{
    [HttpPost("save-user")]
    public async Task<IActionResult> SaveUser([FromBody] User user)
    {
        if (await redisService.KeyExistsAsync(user.Id))
            return Conflict("A user with this ID already exists.");
        
        var saved = await redisService.StringSetAsync(user);
        if (!saved)
            return BadRequest("could not save user");

        return Ok("User saved successfully");
    }
    
    [HttpGet("get-user/{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var userJson = await redisService.StringGetAsync(id);

        if (string.IsNullOrEmpty(userJson))
            return NotFound();

        var user = JsonSerializer.Deserialize<User>(userJson);

        return Ok(user);
    }
}
