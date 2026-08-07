using AeroDesk.Application.Features.CheckIns.Commands.CreateCheckIn;
using AeroDesk.Application.Features.CheckIns.Commands.DeleteCheckIn;
using AeroDesk.Application.Features.CheckIns.Commands.UpdateCheckIn;
using AeroDesk.Application.Features.CheckIns.DTOs;
using AeroDesk.Application.Features.CheckIns.Queries.GetCheckInById;
using AeroDesk.Application.Features.CheckIns.Queries.GetCheckIns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheckInsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CheckInDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetCheckInsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheckInDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCheckInByIdQuery(id));

            if (result == null)
            {
                return NotFound("Check-in not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CheckInDto>> Create(CreateCheckInCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CheckInDto>> Update(
            int id,
            UpdateCheckInCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Check-in not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteCheckInCommand(id));

            if (!deleted)
            {
                return NotFound("Check-in not found.");
            }

            return NoContent();
        }
    }
}