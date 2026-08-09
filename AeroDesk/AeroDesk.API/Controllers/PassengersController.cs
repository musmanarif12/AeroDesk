using AeroDesk.Application.Features.Passengers.Commands.CreatePassenger;
using AeroDesk.Application.Features.Passengers.Commands.DeletePassenger;
using AeroDesk.Application.Features.Passengers.Commands.UpdatePassenger;
using AeroDesk.Application.Features.Passengers.DTOs;
using AeroDesk.Application.Features.Passengers.Queries.GetPassengerById;
using AeroDesk.Application.Features.Passengers.Queries.GetPassengers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator,Check-In Officer")]
    public class PassengersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PassengersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<PassengerDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetPassengersQuery());

            return Ok(result);
        }

        // Passenger allowed here; ownership check (own profile only) happens
        // inside GetPassengerByIdQueryHandler using ICurrentUserService
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Check-In Officer,Passenger")]
        public async Task<ActionResult<PassengerDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPassengerByIdQuery(id));

            if (result == null)
            {
                return NotFound("Passenger not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PassengerDto>> Create(CreatePassengerCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PassengerDto>> Update(
            int id,
            UpdatePassengerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Passenger not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeletePassengerCommand(id));

            if (!result)
            {
                return NotFound("Passenger not found.");
            }

            return NoContent();
        }
    }
}