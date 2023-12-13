using System.IdentityModel.Tokens.Jwt;
using Application.Common;
using MediatR;

namespace Application.Auth.RefreshToken;

public record RefreshTokenRequest(string RefreshToken) : IRequest<Response>;