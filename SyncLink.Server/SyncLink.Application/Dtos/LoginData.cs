namespace SyncLink.Application.Dtos;

public class LoginData
{
    public string UsernameOrEmail { get; set; } = null!;
    public string Password { get; set; } = null!;
}