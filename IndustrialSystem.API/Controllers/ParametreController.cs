using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Parametres;
using IndustrialSystem.Services.Parametres;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ParametreController : BaseController
    {
        private readonly IParametreService _parametreService;

        public ParametreController(IParametreService parametreService)
        {
            _parametreService = parametreService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParametreDto>>> GetAll()
        {
            var parametres = await _parametreService.GetAllAsync();
            return Ok(parametres);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParametreDto>> GetById(int id)
        {
            var parametre = await _parametreService.GetByIdAsync(id);
            return Ok(parametre);
        }

        [HttpGet("global")]
        public async Task<ActionResult<IEnumerable<ParametreDto>>> GetGlobalParameters()
        {
            var parametres = await _parametreService.GetGlobalParametersAsync();
            return Ok(parametres);
        }

        [HttpGet("detailed/poste/{posteId}")]
        public async Task<ActionResult<IEnumerable<ParametreDto>>> GetDetailedParametersByPoste(int posteId)
        {
            var parametres = await _parametreService.GetDetailedParametersByPosteAsync(posteId);
            return Ok(parametres);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ParametreDto>>> Search([FromQuery] string searchTerm)
        {
            var parametres = await _parametreService.SearchAsync(searchTerm);
            return Ok(parametres);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Parametreur")]
        public async Task<ActionResult<ParametreDto>> Create([FromBody] ParametreDto parametreDto)
        {
            var createdParametre = await _parametreService.CreateAsync(parametreDto);
            return CreatedAtAction(nameof(GetById), new { id = createdParametre.Id }, createdParametre);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Parametreur")]
        public async Task<ActionResult<ParametreDto>> Update(int id, [FromBody] ParametreDto parametreDto)
        {
            var updatedParametre = await _parametreService.UpdateAsync(id, parametreDto);
            return Ok(updatedParametre);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _parametreService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("validate")]
        [Authorize(Roles = "Administrateur,Parametreur")]
        public async Task<ActionResult<bool>> ValidateParameterValue([FromQuery] string type, [FromQuery] string value)
        {
            var isValid = await _parametreService.ValidateParameterValueAsync(type, value);
            return Ok(isValid);
        }

        [HttpPut("{id}/value")]
        [Authorize(Roles = "Administrateur,Parametreur")]
        public async Task<ActionResult<ParametreDto>> UpdateParameterValue(int id, [FromBody] string newValue)
        {
            var updatedParametre = await _parametreService.UpdateParameterValueAsync(id, newValue);
            return Ok(updatedParametre);
        }
    }
}