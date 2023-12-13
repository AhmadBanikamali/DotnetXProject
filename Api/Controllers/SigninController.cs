using Application.Auth.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SigninController : ControllerBase
{
    private readonly IMediator _mediator;

    public SigninController(IMediator mediator)
    {
        _mediator = mediator;
    }
 
    [HttpPost]
    public async Task<IActionResult> Signin([FromQuery] string email,[FromQuery] string password)
    {
        var response = await _mediator.Send(new SignInRequest(email, password));
        return new JsonResult(response);
    }
}