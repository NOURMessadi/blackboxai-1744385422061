using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Utilisateurs;
using IndustrialSystem.Services.Users;

namespace IndustrialSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UtilisateursController : BaseController
{
    private readonly IUserService _userService;

    public UtilisateursController(
        IUserService userService,
        ILogger<UtilisateursController> logger)
        : base(logger)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetAll(
        [FromQuery] int? page,
        [FromQuery] int? pageSize)
    {
        var skip = page.HasValue ? (page.Value - 1) * (pageSize ?? 10) : null;
        var take = pageSize;

        var users = await _userService.GetAllAsync(skip, take);
        var totalCount = await _userService.CountAsync();

        return CreatePagedResponse(
            users,
            page ?? 1,
            pageSize ?? 10,
            totalCount);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UtilisateurDto>> GetById(int id)
    {
        if (!IsAdmin() && GetCurrentUserId() != id)
        {
            return Forbid();
        }

        var user = await _userService.GetByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<UtilisateurDto>> Create([FromBody] CreateUtilisateurDto createDto)
    {
        var user = await _userService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UtilisateurDto>> Update(
        int id,
        [FromBody] UpdateUtilisateurDto updateDto)
    {
        if (!IsAdmin() && GetCurrentUserId() != id)
        {
            return Forbid();
        }

        var user = await _userService.UpdateAsync(id, updateDto);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/password")]
    public async Task<ActionResult<UtilisateurDto>> UpdatePassword(
        int id,
        [FromBody] UpdatePasswordDto updatePasswordDto)
    {
        if (!IsAdmin() && GetCurrentUserId() != id)
        {
            return Forbid();
        }

        var user = await _userService.UpdatePasswordAsync(id, updatePasswordDto);
        return Ok(user);
    }

    [HttpPut("{id}/toggle-status")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<UtilisateurDto>> ToggleStatus(int id)
    {
        var user = await _userService.ToggleActiveStatusAsync(id);
        return Ok(user);
    }

    [HttpGet("search")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<IEnumerable<UtilisateurDto>>> Search(
        [FromQuery] string searchTerm,
        [FromQuery] int? page,
        [FromQuery] int? pageSize)
    {
        var skip = page.HasValue ? (page.Value - 1) * (pageSize ?? 10) : null;
        var take = pageSize;

        var users = await _userService.SearchAsync(searchTerm, skip, take);
        return Ok(users);
    }

    [HttpGet("by-role/{role}")]
    [Authorize(Roles = "administrateur")]
    public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetByRole(string role)
    {
        var users = await _userService.GetByRoleAsync(role);
        return Ok(users);
    }

    [HttpGet("check-email")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
    {
        var exists = await _userService.ExistsEmailAsync(email);
        return Ok(exists);
    }

    [HttpGet("check-matricule")]
    [AllowAnonymous]
    public async Task<ActionResult<bool>> CheckMatricule([FromQuery] string matricule)
    {
        var exists = await _userService.ExistsMatriculeAsync(matricule);
        return Ok(exists);
    }
}