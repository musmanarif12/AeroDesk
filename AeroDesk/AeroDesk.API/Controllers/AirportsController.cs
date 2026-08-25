using AeroDesk.Application.Features.Airports.Commands.CreateAirport;
using AeroDesk.Application.Features.Airports.Commands.DeleteAirport;
using AeroDesk.Application.Features.Airports.Commands.UpdateAirport;
using AeroDesk.Application.Features.Airports.DTOs;
using AeroDesk.Application.Features.Airports.Queries.GetAirportById;
using AeroDesk.Application.Features.Airports.Queries.GetAirports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    //[Authorize]
    public class AirportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AirportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/airports
        [HttpGet]
        //[Authorize(Roles = "Administrator,Airline Manager,Check-In Officer")]
        public async Task<ActionResult<List<AirportDto>>> GetAll()
        {
            var airports = await _mediator.Send(
                new GetAirportsQuery());

            return Ok(airports);
        }

        // GET: api/airports/1
        [HttpGet("{id}")]
        //[Authorize(Roles = "Administrator,Airline Manager,Check-In Officer")]
        public async Task<ActionResult<AirportDto>> GetById(int id)
        {
            var airport = await _mediator.Send(
                new GetAirportByIdQuery(id));

            if (airport == null)
            {
                return NotFound("Airport not found.");
            }

            return Ok(airport);
        }

        // POST: api/airports
        [HttpPost]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AirportDto>> Create(
            CreateAirportCommand command)
        {
            var airport = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = airport.Id },
                airport);
        }

        // PUT: api/airports/1
        [HttpPut("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AirportDto>> Update(
            int id,
            UpdateAirportCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var airport = await _mediator.Send(command);

            if (airport == null)
            {
                return NotFound("Airport not found.");
            }

            return Ok(airport);
        }

        // DELETE: api/airports/1
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteAirportCommand(id));

            if (!result)
            {
                return NotFound("Airport not found.");
            }

            return NoContent();
        }
    }
}
