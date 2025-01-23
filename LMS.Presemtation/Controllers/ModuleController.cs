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
    [Route("api/modules")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        // GET: api/modules/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleById(int id)
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
            {
                return NotFound($"Module with ID {id} not found.");
            }
            return Ok(module);
        }

        // POST: api/modules
        [HttpPost]
        public async Task<IActionResult> CreateModule(CreateModuleDto moduleDto)
        {
            if (moduleDto == null)
            {
                return BadRequest("Module data is required.");
            }
            var createdModule = await _moduleService.CreateModuleAsync(moduleDto);
            return CreatedAtAction(nameof(GetModuleById), new { id = createdModule.ModuleId }, createdModule);
        }
        /*
        // PUT: api/modules/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(int id, ModuleDto moduleDto)
        {
            if (moduleDto == null || id != moduleDto.ModuleId)
            {
                return BadRequest("Invalid module data.");
            }

            var isUpdated = await _moduleService.UpdateModuleAsync(id, moduleDto);
            if (!isUpdated)
            {
                return NotFound($"Module with ID {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/modules/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var isDeleted = await _moduleService.DeleteModuleAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Module with ID {id} not found.");
            }

            return NoContent();
        }*/
    }
}
