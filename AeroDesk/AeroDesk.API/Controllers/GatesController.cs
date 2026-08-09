using AeroDesk.Application.Features.Gates.Commands.CreateGate;
using AeroDesk.Application.Features.Gates.Commands.DeleteGate;
using AeroDesk.Application.Features.Gates.Commands.UpdateGate;
using AeroDesk.Application.Features.Gates.DTOs;
using AeroDesk.Application.Features.Gates.Queries.GetGateById;
using AeroDesk.Application.Features.Gates.Queries.GetGates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Airline Manager")]
    public class GatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GateDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetGatesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GateDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetGateByIdQuery(id));

            if (result == null)
                return NotFound("Gate not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GateDto>> Create(CreateGateCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GateDto>> Update(
            int id,
            UpdateGateCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch.");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound("Gate not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteGateCommand(id));

            if (!deleted)
                return NotFound("Gate not found.");

            return NoContent();
        }
    }
}