using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.src.Models;
using PhoneBook.src.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserService _userService;

    public AuthController(IConfiguration config, IUserService userService)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _userService = userService;
    }

    // verify login info 
    [HttpPost("login")]
    public IActionResult Login([FromBody] User login)
    {
        // check if strings are empty
        if (login is null || string.IsNullOrWhiteSpace(login.Login) || string.IsNullOrWhiteSpace(login.Password))
            return BadRequest("Invalid request");

        if (_userService.VerifyUser(login).Result)
        {
            var token = GenerateJwtToken(login.Login);
            return Ok(new { token });
        }
        return Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var result = await _userService.AddUserAsync(user);
        if(result.Result is Created<User> createdUser)
        {
            return CreatedAtAction(nameof(Register), new {login = createdUser.Value.Login}, createdUser.Value);
        }

        return BadRequest("User registration failed.");
    }

    // JWT token generation 
    private string GenerateJwtToken(string username)
    {
        
        var key = _config.GetValue<string>("Jwt:Key") ?? throw new Exception("JWT Key is missing");
        var issuer = _config.GetValue<string>("Jwt:Issuer") ?? "default-issuer";
        var audience = _config.GetValue<string>("Jwt:Audience") ?? "default-audience";
        var tokenValidity = _config.GetValue<int>("Jwt:TokenValidTimeInMinutes", 60);

        // prepare symmetric key and credentials
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
        });

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(
            issuer: issuer,
            audience: audience,
            subject: claimsIdentity,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(tokenValidity),
            signingCredentials: credentials
        );

        var tokenString = handler.WriteToken(token);

        return tokenString;
    }
}
