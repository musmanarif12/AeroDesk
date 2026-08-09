using AeroDesk.Application.Features.Bookings.Commands.CreateBooking;
using AeroDesk.Application.Features.Bookings.Commands.DeleteBooking;
using AeroDesk.Application.Features.Bookings.Commands.UpdateBooking;
using AeroDesk.Application.Features.Bookings.DTOs;
using AeroDesk.Application.Features.Bookings.Queries.GetBookingById;
using AeroDesk.Application.Features.Bookings.Queries.GetBookings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Check-in Officer")]
        public async Task<ActionResult<List<BookingDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetBookingsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Check-in Officer,Passenger")]
        public async Task<ActionResult<BookingDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetBookingByIdQuery(id));

            if (result == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Check-in Officer,Passenger")]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Check-in Officer")]
        public async Task<ActionResult<BookingDto>> Update(
            int id,
            UpdateBookingCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch.");
            }

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound("Booking not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Check-In Officer,CheckInOfficer,Check-in Officer")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteBookingCommand(id));

            if (!deleted)
            {
                return NotFound("Booking not found.");
            }

            return NoContent();
        }
    }
}