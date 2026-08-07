using AeroDesk.Application.Features.BoardingPasses.Commands.CreateBoardingPass;
using AeroDesk.Application.Features.BoardingPasses.Commands.DeleteBoardingPass;
using AeroDesk.Application.Features.BoardingPasses.Commands.UpdateBoardingPass;
using AeroDesk.Application.Features.BoardingPasses.DTOs;
using AeroDesk.Application.Features.BoardingPasses.Queries.GetBoardingPassById;
using AeroDesk.Application.Features.BoardingPasses.Queries.GetBoardingPasses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardingPassesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardingPassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BoardingPassDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetBoardingPassesQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoardingPassDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetBoardingPassByIdQuery(id));

            if (result == null)
            {
                return NotFound("Boarding pass not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BoardingPassDto>> Create(CreateBoardingPassCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BoardingPassDto>> Update(
            int id,
            UpdateBoardingPassCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Boarding pass not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(
                new DeleteBoardingPassCommand(id));

            if (!deleted)
            {
                return NotFound("Boarding pass not found.");
            }

            return NoContent();
        }
    }
}