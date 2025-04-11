using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.Services.Exceptions;

namespace IndustrialSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;

    protected BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Obtient l'ID de l'utilisateur connecté à partir du token JWT
    /// </summary>
    protected int GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new UnauthorizedException("Utilisateur non authentifié ou ID invalide");
        }
        return userId;
    }

    /// <summary>
    /// Obtient le rôle de l'utilisateur connecté à partir du token JWT
    /// </summary>
    protected string GetCurrentUserRole()
    {
        var roleClaim = User.FindFirst(ClaimTypes.Role);
        if (roleClaim == null)
        {
            throw new UnauthorizedException("Rôle utilisateur non trouvé");
        }
        return roleClaim.Value;
    }

    /// <summary>
    /// Obtient l'email de l'utilisateur connecté à partir du token JWT
    /// </summary>
    protected string GetCurrentUserEmail()
    {
        var emailClaim = User.FindFirst(ClaimTypes.Email);
        if (emailClaim == null)
        {
            throw new UnauthorizedException("Email utilisateur non trouvé");
        }
        return emailClaim.Value;
    }

    /// <summary>
    /// Vérifie si l'utilisateur connecté a un rôle spécifique
    /// </summary>
    protected bool HasRole(string role)
    {
        return User.IsInRole(role);
    }

    /// <summary>
    /// Vérifie si l'utilisateur connecté a l'un des rôles spécifiés
    /// </summary>
    protected bool HasAnyRole(params string[] roles)
    {
        return roles.Any(role => User.IsInRole(role));
    }

    /// <summary>
    /// Vérifie si l'utilisateur est administrateur
    /// </summary>
    protected bool IsAdmin()
    {
        return HasRole("administrateur");
    }

    /// <summary>
    /// Vérifie si l'utilisateur est le propriétaire de la ressource ou un administrateur
    /// </summary>
    protected bool CanAccessResource(int resourceUserId)
    {
        return IsAdmin() || GetCurrentUserId() == resourceUserId;
    }

    /// <summary>
    /// Obtient l'adresse IP du client
    /// </summary>
    protected string GetClientIpAddress()
    {
        return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
    }

    /// <summary>
    /// Obtient l'agent utilisateur (User-Agent) du client
    /// </summary>
    protected string GetUserAgent()
    {
        return HttpContext.Request.Headers["User-Agent"].ToString();
    }

    /// <summary>
    /// Crée une réponse paginée
    /// </summary>
    protected ActionResult<PagedResponse<T>> CreatePagedResponse<T>(
        IEnumerable<T> data,
        int pageNumber,
        int pageSize,
        int totalCount,
        string? previousPage = null,
        string? nextPage = null)
    {
        var response = new PagedResponse<T>
        {
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            PreviousPage = previousPage,
            NextPage = nextPage
        };

        return Ok(response);
    }
}

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string? PreviousPage { get; set; }
    public string? NextPage { get; set; }
}