using Application;
using Application.Auth.SignIn;
using Application.Auth.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SignupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post( string email, string password, bool isAdmin)
        {
            var signupTask = await _mediator.Send(new SignUpRequest(email, password, isAdmin));
            if (!signupTask.IsSuccess) return Content(signupTask.Message ?? "");
            var signinTask = await _mediator.Send(new SignInRequest(email, password));
            return new JsonResult(signinTask);
        }
    }
}