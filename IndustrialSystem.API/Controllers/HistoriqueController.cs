using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Historique;
using IndustrialSystem.Services.Historique;

namespace IndustrialSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class HistoriqueController : BaseController
{
    private readonly IHistoriqueService _historiqueService;

    public HistoriqueController(
        IHistoriqueService historiqueService,
        ILogger<HistoriqueController> logger)
        : base(logger)
    {
        _historiqueService = historiqueService;
    }

    [HttpGet]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<IEnumerable<HistoriqueActionDto>>> GetAll(
        [FromQuery] int? page,
        [FromQuery] int? pageSize)
    {
        var skip = page.HasValue ? (page.Value - 1) * (pageSize ?? 10) : null;
        var take = pageSize;

        var historique = await _historiqueService.GetAllAsync(skip, take);
        var totalCount = await _historiqueService.CountAsync();

        return CreatePagedResponse(
            historique,
            page ?? 1,
            pageSize ?? 10,
            totalCount);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<HistoriqueActionDto>> GetById(int id)
    {
        var historique = await _historiqueService.GetByIdAsync(id);
        return Ok(historique);
    }

    [HttpGet("utilisateur/{utilisateurId}")]
    public async Task<ActionResult<IEnumerable<HistoriqueActionDto>>> GetByUtilisateur(int utilisateurId)
    {
        if (!IsAdmin() && GetCurrentUserId() != utilisateurId)
        {
            return Forbid();
        }

        var historique = await _historiqueService.GetByUtilisateurAsync(utilisateurId);
        return Ok(historique);
    }

    [HttpPost("search")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<IEnumerable<HistoriqueActionDto>>> Search(
        [FromBody] RechercheHistoriqueDto searchDto)
    {
        var historique = await _historiqueService.SearchAsync(searchDto);
        return Ok(historique);
    }

    [HttpGet("statistiques")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<StatistiquesHistoriqueDto>> GetStatistiques(
        [FromQuery] DateTime? debut = null,
        [FromQuery] DateTime? fin = null)
    {
        var stats = await _historiqueService.GetStatistiquesAsync(debut, fin);
        return Ok(stats);
    }

    [HttpGet("recent")]
    public async Task<ActionResult<IEnumerable<HistoriqueActionResumeeDto>>> GetRecentActions(
        [FromQuery] int count = 10)
    {
        var actions = await _historiqueService.GetRecentActionsAsync(count);
        return Ok(actions);
    }
}