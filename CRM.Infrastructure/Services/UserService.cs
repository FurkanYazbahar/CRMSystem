using CRM.Application.DTOs;
using CRM.Application.Interfaces;
using CRM.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepo;
    private readonly IConfiguration _config;

    public UserService(IGenericRepository<User> userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<UserDto?> AuthenticateAsync(string username, string password)
    {
        var users = await _userRepo.FindAsync(u => u.Username == username);
        var user = users.FirstOrDefault();

        if (user == null || !VerifyPassword(password, user.PasswordHash))
            return null;

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role,
            Password = "", // şifreyi geri döndürmüyoruz
        };
    }

    public async Task RegisterAsync(UserDto dto)
    {
        var hashed = HashPassword(dto.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            PasswordHash = hashed,
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepo.AddAsync(user);
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hash);
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}
