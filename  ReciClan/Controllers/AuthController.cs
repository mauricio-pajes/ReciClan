using Microsoft.AspNetCore.Mvc;
using ReciClan.Services;

namespace ReciClan.Controllers;

public record LoginReq(string Email, string Password);

public record LoginRes(string Token);

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginReq dto)
    {
        var token = _auth.Login(dto.Email, dto.Password);
        return token is null ? Unauthorized(new { message = "Credencial inválidas" }) : Ok(new LoginRes(token));
    }
}