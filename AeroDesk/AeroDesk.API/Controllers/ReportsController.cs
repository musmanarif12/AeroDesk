using AeroDesk.Application.Features.Reports.Queries.GetFlightsByDate;
using AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirline;
using AeroDesk.Application.Features.Reports.Queries.GetDelayedFlights;
using AeroDesk.Application.Features.Reports.Queries.GetActiveFlights;
using AeroDesk.Application.Features.Reports.Queries.GetFlightsByAirport;
using AeroDesk.Application.Features.Reports.Queries.GetPassengerCountPerFlight;
using AeroDesk.Application.Features.Reports.Queries.GetAvailableSeats;
using AeroDesk.Application.Features.Reports.Queries.GetDailyBookings;
using AeroDesk.Application.Features.Reports.Queries.SearchPassenger;
using AeroDesk.Application.Features.Reports.Queries.GetDashboardSummary;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("flights-by-date")]
        public async Task<IActionResult> GetFlightsByDate([FromQuery] DateTime date)
        {
            var query = new GetFlightsByDateQuery { Date = date };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("flights-by-airline/{airlineId}")]
        public async Task<IActionResult> GetFlightsByAirline(int airlineId)
        {
            var query = new GetFlightsByAirlineQuery { AirlineId = airlineId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("delayed-flights")]
        public async Task<IActionResult> GetDelayedFlights()
        {
            var query = new GetDelayedFlightsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("active-flights")]
        public async Task<IActionResult> GetActiveFlights()
        {
            var query = new GetActiveFlightsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("flights-by-airport/{airportId}")]
        public async Task<IActionResult> GetFlightsByAirport(int airportId)
        {
            var query = new GetFlightsByAirportQuery { AirportId = airportId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("passenger-count-per-flight")]
        public async Task<IActionResult> GetPassengerCountPerFlight()
        {
            var query = new GetPassengerCountPerFlightQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("available-seats")]
        public async Task<IActionResult> GetAvailableSeats()
        {
            var query = new GetAvailableSeatsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("daily-bookings")]
        public async Task<IActionResult> GetDailyBookings()
        {
            var query = new GetDailyBookingsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("search-passenger")]
        public async Task<IActionResult> SearchPassenger([FromQuery] string term)
        {
            var query = new SearchPassengerQuery { SearchTerm = term };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var query = new GetDashboardSummaryQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}