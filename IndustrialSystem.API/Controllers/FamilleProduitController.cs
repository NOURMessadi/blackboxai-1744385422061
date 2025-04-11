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
    public class FamilleProduitController : BaseController
    {
        private readonly IFamilleProduitService _familleProduitService;

        public FamilleProduitController(IFamilleProduitService familleProduitService)
        {
            _familleProduitService = familleProduitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilleProduitDto>>> GetAll()
        {
            var famillesProduits = await _familleProduitService.GetAllAsync();
            return Ok(famillesProduits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FamilleProduitDto>> GetById(int id)
        {
            var familleProduit = await _familleProduitService.GetByIdAsync(id);
            return Ok(familleProduit);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FamilleProduitDto>>> Search([FromQuery] string searchTerm)
        {
            var famillesProduits = await _familleProduitService.SearchAsync(searchTerm);
            return Ok(famillesProduits);
        }

        [HttpGet("type-produit/{typeProduitId}")]
        public async Task<ActionResult<IEnumerable<FamilleProduitDto>>> GetByTypeProduit(int typeProduitId)
        {
            var famillesProduits = await _familleProduitService.GetFamillesByTypeProduitAsync(typeProduitId);
            return Ok(famillesProduits);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<FamilleProduitDto>> Create([FromBody] FamilleProduitDto familleProduitDto)
        {
            var createdFamilleProduit = await _familleProduitService.CreateAsync(familleProduitDto);
            return CreatedAtAction(nameof(GetById), new { id = createdFamilleProduit.Id }, createdFamilleProduit);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<FamilleProduitDto>> Update(int id, [FromBody] FamilleProduitDto familleProduitDto)
        {
            var updatedFamilleProduit = await _familleProduitService.UpdateAsync(id, familleProduitDto);
            return Ok(updatedFamilleProduit);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _familleProduitService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/in-use")]
        public async Task<ActionResult<bool>> IsFamilleInUse(int id)
        {
            var isInUse = await _familleProduitService.IsFamilleInUseAsync(id);
            return Ok(isInUse);
        }

        [HttpGet("{id}/product-count")]
        public async Task<ActionResult<int>> GetProductCount(int id)
        {
            var count = await _familleProduitService.GetProductCountByFamilleAsync(id);
            return Ok(count);
        }
    }
}