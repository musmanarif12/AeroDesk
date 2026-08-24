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
    [AllowAnonymous]
    // [Authorize]
    public class PassengersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PassengersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/passengers
        [HttpGet]
        // [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Boarding Officer,BoardingOfficer")]
        public async Task<ActionResult<List<PassengerDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetPassengersQuery());
            return Ok(result);
        }

        // GET: api/passengers/1
        [HttpGet("{id}")]
        // [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Boarding Officer,BoardingOfficer,Passenger")]
        public async Task<ActionResult<PassengerDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPassengerByIdQuery(id));

            if (result == null)
            {
                return NotFound("Passenger not found.");
            }

            return Ok(result);
        }

        // POST: api/passengers
        [HttpPost]
        // [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer")]
        public async Task<ActionResult<PassengerDto>> Create(CreatePassengerCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        // PUT: api/passengers/1
        [HttpPut("{id}")]
        // [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer")]
        public async Task<ActionResult<PassengerDto>> Update(int id, UpdatePassengerCommand command)
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

        // DELETE: api/passengers/1
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeletePassengerCommand(id));

            if (!deleted)
            {
                return NotFound("Passenger not found.");
            }

            return NoContent();
        }
    }
}