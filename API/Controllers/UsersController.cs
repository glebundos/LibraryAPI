using API.Controllers.Base;
using Application.Commands;
using Application.Helpers;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateCommand command)
        {
            return await CommandAsync(command);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticateResponse>> Register([FromBody] RegistrateCommand command)
        {
            return await CommandAsync(command);
        }
    }
}
