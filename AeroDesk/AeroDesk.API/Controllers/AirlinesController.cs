using AeroDesk.Application.Features.Airlines.Commands.CreateAirline;
using AeroDesk.Application.Features.Airlines.Commands.DeleteAirline;
using AeroDesk.Application.Features.Airlines.Commands.UpdateAirline;
using AeroDesk.Application.Features.Airlines.DTOs;
using AeroDesk.Application.Features.Airlines.Queries.GetAirlineById;
using AeroDesk.Application.Features.Airlines.Queries.GetAirlines;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AirlinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AirlinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/airlines
        [HttpGet]
        [Authorize(Roles = "Administrator,Airline Manager,AirlineManager")]
        public async Task<ActionResult<List<AirlineDto>>> GetAll()
        {
            var airlines = await _mediator.Send(new GetAirlinesQuery());

            return Ok(airlines);
        }

        // GET: api/airlines/1
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Airline Manager,AirlineManager")]
        public async Task<ActionResult<AirlineDto>> GetById(int id)
        {
            var airline = await _mediator.Send(new GetAirlineByIdQuery(id));

            if (airline == null)
            {
                return NotFound("Airline not found.");
            }

            return Ok(airline);
        }

        // POST: api/airlines
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AirlineDto>> Create(
            CreateAirlineCommand command)
        {
            var airline = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = airline.Id },
                airline);
        }

        // PUT: api/airlines/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AirlineDto>> Update(
            int id,
            UpdateAirlineCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var airline = await _mediator.Send(command);

            if (airline == null)
            {
                return NotFound("Airline not found.");
            }

            return Ok(airline);
        }

        // DELETE: api/airlines/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(
                new DeleteAirlineCommand(id));

            if (!result)
            {
                return NotFound("Airline not found.");
            }

            return NoContent();
        }
    }
}