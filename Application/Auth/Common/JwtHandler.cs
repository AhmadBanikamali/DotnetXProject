using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth.Common;

public class JwtHandler
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public JwtHandler(UserManager<ApplicationUser> userManager,IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<string> CreateJwtToken(ApplicationUser user)
    {
        var rolesAsync = await _userManager.GetRolesAsync(user);
        var claims = new[]
        {
            new Claim(type: ClaimTypes.Email, value: user.UserName!),
            new Claim(type: ClaimTypes.Role, value: rolesAsync.First()),
        };
        var symmetricSecurityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? string.Empty));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:DurationInMinutes"])),
            signingCredentials: signingCredentials
        );
 
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        
    }
    
    
    
}