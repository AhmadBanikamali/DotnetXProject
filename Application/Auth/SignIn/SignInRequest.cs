using Application.Common;
using MediatR;

namespace Application.Auth.SignIn;

public record SignInRequest(string Email, string Password) : IRequest<Response>;