using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Operations;
using IndustrialSystem.Services.Operations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IndustrialSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OperationController : BaseController
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetAll()
        {
            var operations = await _operationService.GetAllAsync();
            return Ok(operations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationDto>> GetById(int id)
        {
            var operation = await _operationService.GetByIdAsync(id);
            return Ok(operation);
        }

        [HttpGet("produit/{produitId}")]
        public async Task<ActionResult<IEnumerable<OperationDto>>> GetByProduit(int produitId)
        {
            var operations = await _operationService.GetByProduitAsync(produitId);
            return Ok(operations);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<OperationDto>>> Search([FromQuery] string searchTerm)
        {
            var operations = await _operationService.SearchAsync(searchTerm);
            return Ok(operations);
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<OperationDto>> Create([FromBody] OperationDto operationDto)
        {
            var createdOperation = await _operationService.CreateAsync(operationDto);
            return CreatedAtAction(nameof(GetById), new { id = createdOperation.Id }, createdOperation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<OperationDto>> Update(int id, [FromBody] OperationDto operationDto)
        {
            var updatedOperation = await _operationService.UpdateAsync(id, operationDto);
            return Ok(updatedOperation);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<ActionResult> Delete(int id)
        {
            await _operationService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("validate-sequence/{produitId}")]
        [Authorize(Roles = "Administrateur,Preparateur")]
        public async Task<ActionResult<bool>> ValidateOperationSequence(int produitId, [FromBody] List<int> operationIds)
        {
            var isValid = await _operationService.ValidateOperationSequenceAsync(produitId, operationIds);
            return Ok(isValid);
        }
    }
}