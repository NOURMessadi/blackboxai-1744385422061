using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Postes;
using IndustrialSystem.Services.Postes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TypePosteController : BaseController
    {
        private readonly ITypePosteService _typePosteService;

        public TypePosteController(ITypePosteService typePosteService)
        {
            _typePosteService = typePosteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypePosteDto>>> GetAll()
        {
            var typePostes = await _typePosteService.GetAllAsync();
            return Ok(typePostes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypePosteDto>> GetById(int id)
        {
            var typePoste = await _typePosteService.GetByIdAsync(id);
            return Ok(typePoste);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TypePosteDto>>> Search([FromQuery] string searchTerm)
        {
            var typePostes = await _typePosteService.SearchAsync(searchTerm);
            return Ok(typePostes);
        }

        [HttpGet("with-postes")]
        public async Task<ActionResult<IEnumerable<TypePosteDto>>> GetTypesWithPostes()
        {
            var typePostes = await _typePosteService.GetTypesWithPostesAsync();
            return Ok(typePostes);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<TypePosteDto>> Create([FromBody] TypePosteDto typePosteDto)
        {
            var createdTypePoste = await _typePosteService.CreateAsync(typePosteDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTypePoste.Id }, createdTypePoste);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<TypePosteDto>> Update(int id, [FromBody] TypePosteDto typePosteDto)
        {
            var updatedTypePoste = await _typePosteService.UpdateAsync(id, typePosteDto);
            return Ok(updatedTypePoste);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _typePosteService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/in-use")]
        public async Task<ActionResult<bool>> IsTypeInUse(int id)
        {
            var isInUse = await _typePosteService.IsTypeInUseAsync(id);
            return Ok(isInUse);
        }

        [HttpGet("{id}/poste-count")]
        public async Task<ActionResult<int>> GetPosteCount(int id)
        {
            var count = await _typePosteService.GetPosteCountByTypeAsync(id);
            return Ok(count);
        }

        [HttpPost("validate")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult<bool>> ValidateTypePoste([FromQuery] string nom)
        {
            var isValid = await _typePosteService.ValidateTypePosteAsync(nom);
            return Ok(isValid);
        }
    }
}