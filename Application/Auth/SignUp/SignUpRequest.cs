using Application.Common;
using MediatR;

namespace Application.Auth.SignUp;

public record SignUpRequest(string Email, string Password, bool IsAdmin) : IRequest<Response>;