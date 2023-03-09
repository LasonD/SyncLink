namespace SyncLink.Application.Dtos;

public class RegistrationData
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = null!;
}