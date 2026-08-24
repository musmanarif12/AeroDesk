using AeroDesk.Application.Features.Documents.Commands.UploadDocument;
using AeroDesk.Application.Features.Documents.Queries.DownloadDocument;
using AeroDesk.Application.Features.Documents.Queries.GetDocumentsByEntity;
using AeroDesk.Application.Features.Documents.Commands.DeleteDocument;
using AeroDesk.Application.Features.Documents.Commands.UpdateDocument;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeroDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize] // Base: must be logged in. Passenger can upload own docs (SRS 4.5)
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Every role may upload — Administrator, Check-In Officer, Airline Manager, Passenger
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(
            [FromForm] IFormFile file,
            [FromForm] string entityType,
            [FromForm] int entityId,
            [FromForm] int uploadedByUserId)
        {
            var command = new UploadDocumentCommand
            {
                File = file,
                EntityType = entityType,
                EntityId = entityId,
                UploadedByUserId = uploadedByUserId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // Passenger allowed here; ownership check (own document only) happens
        // inside DownloadDocumentQueryHandler using ICurrentUserService
        [HttpGet("{id}/download")]
        [Authorize(Roles = "Administrator,Check-In Officer,Airline Manager,Passenger")]
        public async Task<IActionResult> Download(int id)
        {
            var query = new DownloadDocumentQuery { Id = id };
            var result = await _mediator.Send(query);

            return File(result.FileStream, result.ContentType, result.FileName);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Check-In Officer,Airline Manager")]
        public async Task<IActionResult> GetDocumentsByEntity(
        [FromQuery] string entityType,
        [FromQuery] int entityId)
        {
            var query = new GetDocumentsByEntityQuery { EntityType = entityType, EntityId = entityId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator,Check-In Officer,Airline Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDocumentCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Check-In Officer,Airline Manager")]
        public async Task<IActionResult> Update(
    int id,
    [FromForm] IFormFile file,
    [FromForm] int uploadedByUserId)
        {
            var command = new UpdateDocumentCommand
            {
                Id = id,
                File = file,
                UploadedByUserId = uploadedByUserId
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}