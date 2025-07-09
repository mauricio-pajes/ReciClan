namespace ReciClan.Services;

public interface IAuthService
{
    string? Login(string email, string password);
}

public record Usuario(string Email, string Password);

public class AuthService : IAuthService
{
    private static readonly List<Usuario> _users =
    [
        new("user@test.com", "valid123"),
        new("admin@test.com", "admin123")
    ];

    public string? Login(string email, string password) => _users.Any(u => u.Email == email && u.Password == password)
        ? $"TOKEN-{Guid.NewGuid()}"
        : null;
}