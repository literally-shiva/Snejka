using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SnejkaDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthController(
        SnejkaDbContext dbContext,
        IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(User user)
    {
        var username = user.Username.Trim();

        if (string.IsNullOrWhiteSpace(username))
        {
            return BadRequest(new
            {
                message = "Username is required."
            });
        }

        if (string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            return BadRequest(new
            {
                message = "Password is required."
            });
        }

        var usernameExists = await _dbContext.Users
            .AnyAsync(user => user.Username == username);

        if (usernameExists)
        {
            return Conflict(new
            {
                message = "Username is already taken."
            });
        }

        var user1 = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = _passwordHasher.HashPassword(
                user,
                user.PasswordHash)
        };

        _dbContext.Users.Add(user1);

        await _dbContext.SaveChangesAsync();

        return Created("", new
        {
            user.Id,
            user.Username
        });
    }
}