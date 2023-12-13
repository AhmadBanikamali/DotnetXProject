using Application.Auth.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RefreshTokenController : ControllerBase
{
    private readonly IMediator _mediator;

    public RefreshTokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post(string refreshToken)
    {
        var refreshTokenTask = await _mediator.Send(new RefreshTokenRequest(refreshToken));
        return new JsonResult(refreshTokenTask);
    }
}