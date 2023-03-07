using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Dtos;
using SyncLink.Infrastructure.Data.Models.Identity;
using SyncLink.Infrastructure.Extensions;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SyncLink.Infrastructure.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly IConfiguration _config;
    private readonly UserManager<SyncLinkIdentityUser> _userManager;

    public AuthRepository(UserManager<SyncLinkIdentityUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<RepositoryEntityResult<AuthResult>> AuthenticateUserAsync(LoginData loginData, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _userManager.FindByNameAsync(loginData.UsernameOrEmail) ?? 
                   await _userManager.FindByEmailAsync(loginData.UsernameOrEmail);

        if (user == null)
        {
            return RepositoryEntityResult<AuthResult>.NotFound();
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginData.Password);

        if (!isValidPassword)
        {
            return RepositoryEntityResult<AuthResult>.NotFound();
        }

        var authResult = await PrepareAuthResultAsync(user);

        return RepositoryEntityResult<AuthResult>.Ok(authResult);
    }

    public async Task<RepositoryEntityResult<AuthResult>> RegisterUserAsync(RegistrationData registrationData, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var userName = string.IsNullOrWhiteSpace(registrationData.UserName)
            ? $"{registrationData.FirstName}_{registrationData.LastName}"
            : registrationData.UserName; 

        var newUser = new SyncLinkIdentityUser()
        {
            Email = registrationData.Email,
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName,
            UserName = userName,
        };

        var identityResult = await _userManager.CreateAsync(newUser, registrationData.Password);

        if (!identityResult.Succeeded)
        {
            var repositoryErrors = identityResult
                .Errors
                .Select(e => new RepositoryError(e.Description, e.Code));

            return RepositoryEntityResult<AuthResult>.Conflict(repositoryErrors);
        }

        var authResult = await PrepareAuthResultAsync(newUser);

        return RepositoryEntityResult<AuthResult>.Ok(authResult);
    }

    #region Private methods

    private async Task<AuthResult> PrepareAuthResultAsync(SyncLinkIdentityUser user)
    {
        var tokenClaims = await CollectClaimsAsync(user);
        var tokenStr = GenerateToken(tokenClaims);

        var authResult = new AuthResult()
        {
            UserId = user.Id,
            Username = user.UserName,
            Email = user.Email,
            AccessToken = tokenStr,
            FirstName = user.FirstName,
            LastName = user.LastName,
        };

        return authResult;
    }

    private string GenerateToken(IEnumerable<Claim> tokenClaims)
    {
        var tokenDurationMinutes = _config.GetTokenDurationMinutes();
        var issuer = _config.GetIssuer();
        var audience = _config.GetAudience();
        var tokenGenerationKey = _config.GetTokenGenerationKey();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenGenerationKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config[issuer],
            audience: _config[audience],
            claims: tokenClaims,
            expires: DateTime.UtcNow.AddMinutes(tokenDurationMinutes),
            signingCredentials: credentials
        );

        var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenStr;
    }

    private async Task<List<Claim>> CollectClaimsAsync(SyncLinkIdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        var tokenClaims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }
            .Union(claims)
            .Union(roles.Select(r => new Claim(ClaimTypes.Role, r)))
            .ToList();

        if (user.Email != null)
        {
            tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        }

        return tokenClaims;
    }

    #endregion
}