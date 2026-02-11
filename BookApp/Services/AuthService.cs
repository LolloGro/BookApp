using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookApp.Services;

public class AuthService(BookDb database, IConfiguration configuration) : IAuthService
{
    
    public User Register(UserRegisterDto user)
    {
        if (database.Users.Any(x => x.Username == user.Username))
        {
            throw new Exception("Username already exists");
        }

        if (database.Users.Any(x => x.Email == user.Email))
        {
            throw new Exception("Email already exists");
        }

        var registerUser = new User
        {
            Username = user.Username,
            Email = user.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
        };
        
        database.Users.Add(registerUser);
        database.SaveChanges();
        return registerUser;
    }

    public string Login(UserLoginDto user)
    {
        var loginUser = database.Users.FirstOrDefault(u => u.Username == user.Username);
        
        if (loginUser == null)
        {
            throw new UnauthorizedAccessException("User not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(user.Password, loginUser.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        
        return GenerateJwtToken(loginUser);
    }

    private string GenerateJwtToken(User user)
    { 
        //hemlig nyckel 
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };
        
        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience:configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: credentials
        );
        //skapar token som innehåller information om användaren, giltighetstid och signering 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}