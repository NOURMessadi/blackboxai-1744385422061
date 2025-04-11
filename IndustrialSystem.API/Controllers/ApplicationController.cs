using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Applications;
using IndustrialSystem.Services.Applications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : BaseController
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAll()
        {
            var applications = await _applicationService.GetAllAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> GetById(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            return Ok(application);
        }

        [HttpGet("poste/{posteId}")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetByPosteId(int posteId)
        {
            var applications = await _applicationService.GetByPosteIdAsync(posteId);
            return Ok(applications);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<ApplicationDto>> Create([FromBody] ApplicationDto applicationDto)
        {
            var createdApplication = await _applicationService.CreateAsync(applicationDto);
            return CreatedAtAction(nameof(GetById), new { id = createdApplication.Id }, createdApplication);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<ApplicationDto>> Update(int id, [FromBody] ApplicationDto applicationDto)
        {
            var updatedApplication = await _applicationService.UpdateAsync(id, applicationDto);
            return Ok(updatedApplication);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _applicationService.DeleteAsync(id);
            return NoContent();
        }
    }
}