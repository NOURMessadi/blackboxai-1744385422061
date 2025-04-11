using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Nomenclatures;
using IndustrialSystem.Services.Nomenclatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NomenclatureController : BaseController
    {
        private readonly INomenclatureService _nomenclatureService;

        public NomenclatureController(INomenclatureService nomenclatureService)
        {
            _nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NomenclatureDto>>> GetAll()
        {
            var nomenclatures = await _nomenclatureService.GetAllAsync();
            return Ok(nomenclatures);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NomenclatureDto>> GetById(int id)
        {
            var nomenclature = await _nomenclatureService.GetByIdAsync(id);
            return Ok(nomenclature);
        }

        [HttpGet("produit/{produitId}")]
        public async Task<ActionResult<IEnumerable<NomenclatureDto>>> GetByProduit(int produitId)
        {
            var nomenclatures = await _nomenclatureService.GetByProduitAsync(produitId);
            return Ok(nomenclatures);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<NomenclatureDto>> Create([FromBody] NomenclatureDto nomenclatureDto)
        {
            var createdNomenclature = await _nomenclatureService.CreateAsync(nomenclatureDto);
            return CreatedAtAction(nameof(GetById), new { id = createdNomenclature.Id }, createdNomenclature);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<NomenclatureDto>> Update(int id, [FromBody] NomenclatureDto nomenclatureDto)
        {
            var updatedNomenclature = await _nomenclatureService.UpdateAsync(id, nomenclatureDto);
            return Ok(updatedNomenclature);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _nomenclatureService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("validate-composants/{produitId}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<bool>> ValidateComposants(int produitId, [FromBody] Dictionary<int, decimal> composantsQuantites)
        {
            var isValid = await _nomenclatureService.ValidateComposantsAsync(produitId, composantsQuantites);
            return Ok(isValid);
        }

        [HttpGet("composant/{composantId}/quantite-totale")]
        public async Task<ActionResult<decimal>> GetQuantiteTotaleComposant(int composantId)
        {
            var quantiteTotale = await _nomenclatureService.CalculerQuantiteTotaleComposantAsync(composantId);
            return Ok(quantiteTotale);
        }
    }
}