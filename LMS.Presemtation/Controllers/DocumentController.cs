using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Presemtation.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        // GET: api/documents
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        // GET: api/documents/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound($"Document with ID {id} not found.");
            }
            return Ok(document);
        }

        // POST: api/documents
        [HttpPost]
        public async Task<IActionResult> CreateDocument(CreateDocumentDto createDocumentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentDto = new DocumentDto
            {
                Name = createDocumentDto.Name,
                Description = createDocumentDto.Description,
                FilePath = createDocumentDto.FilePath,
                Type = createDocumentDto.Type,
                ModuleId = createDocumentDto.ModuleId,
                CourseId = createDocumentDto.CourseId,
                ActivityId = createDocumentDto.ActivityId,
                TimeStamp = DateTime.UtcNow,
                ApplicationUserId = "default_user" // Replace with actual user ID if available
            };

            await _documentService.CreateDocumentAsync(documentDto);

            return CreatedAtAction(nameof(GetDocumentById), new { id = documentDto.Id }, documentDto);
        }

        // PUT: api/documents/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, UpdateDocumentDto updateDocumentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentDto = new DocumentDto
            {
                Id = id,
                Name = updateDocumentDto.Name,
                Description = updateDocumentDto.Description,
                FilePath = updateDocumentDto.FilePath,
                Type = updateDocumentDto.Type,
                TimeStamp = DateTime.UtcNow // Update timestamp on edit
            };

            var isUpdated = await _documentService.UpdateDocumentAsync(id, documentDto);
            if (!isUpdated)
            {
                return NotFound($"Document with ID {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/documents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var isDeleted = await _documentService.DeleteDocumentAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Document with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
