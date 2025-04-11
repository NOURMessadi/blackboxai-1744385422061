using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Produits;
using IndustrialSystem.Services.Produits;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TypeProduitController : BaseController
    {
        private readonly ITypeProduitService _typeProduitService;

        public TypeProduitController(ITypeProduitService typeProduitService)
        {
            _typeProduitService = typeProduitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduitDto>>> GetAll()
        {
            var typesProduits = await _typeProduitService.GetAllAsync();
            return Ok(typesProduits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduitDto>> GetById(int id)
        {
            var typeProduit = await _typeProduitService.GetByIdAsync(id);
            return Ok(typeProduit);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TypeProduitDto>>> Search([FromQuery] string searchTerm)
        {
            var typesProduits = await _typeProduitService.SearchAsync(searchTerm);
            return Ok(typesProduits);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<TypeProduitDto>> Create([FromBody] TypeProduitDto typeProduitDto)
        {
            var createdTypeProduit = await _typeProduitService.CreateAsync(typeProduitDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTypeProduit.Id }, createdTypeProduit);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<TypeProduitDto>> Update(int id, [FromBody] TypeProduitDto typeProduitDto)
        {
            var updatedTypeProduit = await _typeProduitService.UpdateAsync(id, typeProduitDto);
            return Ok(updatedTypeProduit);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _typeProduitService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/in-use")]
        public async Task<ActionResult<bool>> IsTypeInUse(int id)
        {
            var isInUse = await _typeProduitService.IsTypeInUseAsync(id);
            return Ok(isInUse);
        }

        [HttpGet("{id}/product-count")]
        public async Task<ActionResult<int>> GetProductCount(int id)
        {
            var count = await _typeProduitService.GetProductCountByTypeAsync(id);
            return Ok(count);
        }
    }
}