using Application.Auth.Common;
using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Repository;

namespace Application.Auth.RefreshToken;

public class RefreshTokenService : DbMapperProvider, IRequestHandler<RefreshTokenRequest, Response>
{
    private readonly JwtHandler _jwtHandler;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenService(JwtHandler jwtHandler, IMapper mapper, IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager) : base(mapper, unitOfWork)
    {
        _jwtHandler = jwtHandler;
        _userManager = userManager;
    }

    public async Task<Response> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = (await UnitOfWork.TokenRepository.Get(x => x.RefreshToken == request.RefreshToken))
            .FirstOrDefault();
        var tokenNotFound = refreshToken == null;
        var tokenIsNotValid = refreshToken is not { IsValid: true };
        var tokenExpired = refreshToken!.ExpireAt < DateTime.Now;


        if (tokenNotFound || tokenIsNotValid)
            return new Response(Message: "invalid refresh token", IsSuccess: false);
        
        if(tokenExpired)
            return new Response(Message: "refresh token expired, login again.", IsSuccess: false);
            

        var user = await _userManager.FindByIdAsync(refreshToken!.ApplicationUserId);
        if (user == null)
        {
            return new Response(Message: "user not found", IsSuccess: false);
        }
        var token = await _jwtHandler.CreateJwtToken(user);
        
        return new Response(Data: new AuthInfo(AccessToken: token, RefreshToken: refreshToken.RefreshToken));
    }
}