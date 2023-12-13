using Application.Auth.Common;
using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository;

namespace Application.Auth.SignIn;

public class SignInService : DbMapperProvider, IRequestHandler<SignInRequest, Response>
{
    private readonly JwtHandler _jwtHandler;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public SignInService(JwtHandler jwtHandler, UserManager<ApplicationUser> userManager, IMapper mapper,
        IUnitOfWork unitOfWork,
        IConfiguration configuration) : base(mapper, unitOfWork)
    {
        _jwtHandler = jwtHandler;
        _userManager = userManager;
        _configuration = configuration;
    }


    public async Task<Response> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Email);
        if (user == null)
        {
            return new Response(Message: $"No Accounts Registered with {request.Email}.", IsSuccess: false);
        }

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return new Response(IsSuccess: false, Message: $"Incorrect Credentials for user {request.Email}.");

        var token = await _jwtHandler.CreateJwtToken(user);
        var refreshToken = Guid.NewGuid().ToString();

        UnitOfWork.CreateTransaction();
        var currentToken = (await UnitOfWork.TokenRepository.Get(x => x.ApplicationUserId == user.Id)).FirstOrDefault();

        if (currentToken != null)
            await UnitOfWork.TokenRepository.Delete(currentToken);

        await UnitOfWork.TokenRepository.Insert(new Token
        {
            ExpireAt = DateTime.Now.Add(TimeSpan.FromDays(2)),
            IsValid = true,
            RefreshToken = refreshToken,
            ApplicationUserId = user.Id,
        });

        await UnitOfWork.Save();

        UnitOfWork.Commit();
        return new Response(Data: new AuthInfo(AccessToken: token, RefreshToken: refreshToken));
    }
}