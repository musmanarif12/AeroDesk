using AeroDesk.Application.Features.Flights.Commands.CreateFlight;
using AeroDesk.Application.Features.Flights.Commands.DeleteFlight;
using AeroDesk.Application.Features.Flights.Commands.UpdateFlight;
using AeroDesk.Application.Features.Flights.DTOs;
using AeroDesk.Application.Features.Flights.Queries.GetFlightById;
using AeroDesk.Application.Features.Flights.Queries.GetFlights;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<FlightDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetFlightsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FlightDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetFlightByIdQuery(id));

            if (result == null)
            {
                return NotFound("Flight not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<FlightDto>> Create(CreateFlightCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FlightDto>> Update(
            int id,
            UpdateFlightCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Flight not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(
                new DeleteFlightCommand(id));

            if (!deleted)
            {
                return NotFound("Flight not found.");
            }

            return NoContent();
        }
    }
}