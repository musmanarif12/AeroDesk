using AeroDesk.Application.Features.Aircrafts.Commands.CreateAircraft;
using AeroDesk.Application.Features.Aircrafts.Commands.DeleteAircraft;
using AeroDesk.Application.Features.Aircrafts.Commands.UpdateAircraft;
using AeroDesk.Application.Features.Aircrafts.DTOs;
using AeroDesk.Application.Features.Aircrafts.Queries.GetAircraftById;
using AeroDesk.Application.Features.Aircrafts.Queries.GetAircrafts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    //[Authorize]
    public class AircraftsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AircraftsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/aircrafts
        [HttpGet]
        //[Authorize(Roles = "Administrator,Airline Manager")]
        public async Task<ActionResult<List<AircraftDto>>> GetAll()
        {
            var aircrafts = await _mediator.Send(new GetAircraftsQuery());

            return Ok(aircrafts);
        }

        // GET: api/aircrafts/1
        [HttpGet("{id}")]
        //[Authorize(Roles = "Administrator,Airline Manager")]
        public async Task<ActionResult<AircraftDto>> GetById(int id)
        {
            var aircraft = await _mediator.Send(new GetAircraftByIdQuery(id));

            if (aircraft == null)
            {
                return NotFound("Aircraft not found.");
            }

            return Ok(aircraft);
        }

        // POST: api/aircrafts
        [HttpPost]
        //[Authorize(Roles = "Administrator,Airline Manager")]
        public async Task<ActionResult<AircraftDto>> Create(CreateAircraftCommand command)
        {
            var aircraft = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = aircraft.Id },
                aircraft);
        }

        // PUT: api/aircrafts/1
        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator,Airline Manager")]
        public async Task<ActionResult<AircraftDto>> Update(
            int id,
            UpdateAircraftCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var aircraft = await _mediator.Send(command);

            if (aircraft == null)
            {
                return NotFound("Aircraft not found.");
            }

            return Ok(aircraft);
        }

        // DELETE: api/aircrafts/1
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteAircraftCommand(id));

            if (!result)
            {
                return NotFound("Aircraft not found.");
            }

            return NoContent();
        }
    }
}
