using System.Diagnostics;
using Application.Common;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.SignUp;

public class SignUpService : IRequestHandler<SignUpRequest, Response>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SignUpService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Response> Handle(SignUpRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser()
        {
            UserName = request.Email,
        };

        var identityRoles = _roleManager.Roles.ToList();
        var findByNameAsync = await _userManager.FindByNameAsync(request.Email);
        if (findByNameAsync != null) return new Response(IsSuccess: false, Message: "duplicate user");
        
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return new Response(IsSuccess: false, Message: "add user problem");
        var administrator = identityRoles[0];
        var normalUser = identityRoles[1];
        var role = request.IsAdmin ? administrator : normalUser;
        
        var addToRoleAsync = await _userManager.AddToRoleAsync(user, role.Name!);
        
        return addToRoleAsync.Succeeded ? new Response() : new Response(IsSuccess: false, Message: "add role problem");
    }
}