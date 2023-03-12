namespace SyncLink.Server.Dtos;

public class AuthResultDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
}