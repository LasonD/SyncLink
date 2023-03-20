namespace SyncLink.Application.Dtos;

public class AuthResult
{
    public string IdentityId { get; set; } = null!;
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Email { get; set; }
    public string AccessToken { get; set; } = null!;
    public long ExpiresIn { get; set; }
}