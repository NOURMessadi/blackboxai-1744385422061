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
    public class ProduitController : BaseController
    {
        private readonly IProduitService _produitService;

        public ProduitController(IProduitService produitService)
        {
            _produitService = produitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetAll()
        {
            var produits = await _produitService.GetAllAsync();
            return Ok(produits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProduitDto>> GetById(int id)
        {
            var produit = await _produitService.GetByIdAsync(id);
            return Ok(produit);
        }

        [HttpGet("type/{typeProduitId}")]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetByTypeProduit(int typeProduitId)
        {
            var produits = await _produitService.GetByTypeProduitAsync(typeProduitId);
            return Ok(produits);
        }

        [HttpGet("famille/{familleProduitId}")]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> GetByFamilleProduit(int familleProduitId)
        {
            var produits = await _produitService.GetByFamilleProduitAsync(familleProduitId);
            return Ok(produits);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProduitDto>>> Search([FromQuery] string searchTerm)
        {
            var produits = await _produitService.SearchAsync(searchTerm);
            return Ok(produits);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<ProduitDto>> Create([FromBody] ProduitDto produitDto)
        {
            var createdProduit = await _produitService.CreateAsync(produitDto);
            return CreatedAtAction(nameof(GetById), new { id = createdProduit.Id }, createdProduit);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<ProduitDto>> Update(int id, [FromBody] ProduitDto produitDto)
        {
            var updatedProduit = await _produitService.UpdateAsync(id, produitDto);
            return Ok(updatedProduit);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _produitService.DeleteAsync(id);
            return NoContent();
        }
    }
}