using AeroDesk.Application.Features.Auth.Commands.AdminRegister;
using AeroDesk.Application.Features.Auth.Commands.Login;
using AeroDesk.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Passenger Registration
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Administrator Registration
        [AllowAnonymous] // Temporary (only for initial setup)
        [HttpPost("admin-register")]
        public async Task<IActionResult> AdminRegister(AdminRegisterCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}