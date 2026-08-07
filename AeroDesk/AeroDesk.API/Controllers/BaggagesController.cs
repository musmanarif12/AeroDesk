using AeroDesk.Application.Features.Baggages.Commands.CreateBaggage;
using AeroDesk.Application.Features.Baggages.Commands.DeleteBaggage;
using AeroDesk.Application.Features.Baggages.Commands.UpdateBaggage;
using AeroDesk.Application.Features.Baggages.DTOs;
using AeroDesk.Application.Features.Baggages.Queries.GetBaggageById;
using AeroDesk.Application.Features.Baggages.Queries.GetBaggages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaggagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaggagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BaggageDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetBaggagesQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaggageDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetBaggageByIdQuery(id));

            if (result == null)
            {
                return NotFound("Baggage not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BaggageDto>> Create(CreateBaggageCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaggageDto>> Update(
            int id,
            UpdateBaggageCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Baggage not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteBaggageCommand(id));

            if (!result)
            {
                return NotFound("Baggage not found.");
            }

            return NoContent();
        }
    }
}