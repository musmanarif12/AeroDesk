using AeroDesk.Application.Features.Flights.Commands.CreateFlight;
using AeroDesk.Application.Features.Flights.Commands.DeleteFlight;
using AeroDesk.Application.Features.Flights.Commands.UpdateFlight;
using AeroDesk.Application.Features.Flights.DTOs;
using AeroDesk.Application.Features.Flights.Queries.GetFlightById;
using AeroDesk.Application.Features.Flights.Queries.GetFlights;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AeroDesk.Application.Common; 

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]   <-- temporarily comment out for testing
    [AllowAnonymous]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<FlightDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 8)
        {
            var query = new GetFlightsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _mediator.Send(query);
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
        //[Authorize(Roles = "Administrator,Airline Manager,AirlineManager")]
        public async Task<ActionResult<FlightDto>> Create(CreateFlightCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Airline Manager,AirlineManager")]
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
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteFlightCommand(id));

            if (!deleted)
            {
                return NotFound("Flight not found.");
            }

            return NoContent();
        }
    }
}