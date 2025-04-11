using Microsoft.AspNetCore.Mvc;
using IndustrialSystem.DTOs.Auth;
using IndustrialSystem.Services.Auth;
using IndustrialSystem.Services.Exceptions;

namespace IndustrialSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authentifie un utilisateur et retourne un token JWT
    /// </summary>
    /// <param name="loginDto">Les informations de connexion</param>
    /// <returns>Un token JWT et les informations de l'utilisateur</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginDto)
    {
        try
        {
            var result = await _authService.LoginAsync(loginDto);
            return Ok(result);
        }
        catch (UnauthorizedException ex)
        {
            _logger.LogWarning("Tentative de connexion échouée pour l'email: {Email}", loginDto.Email);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la tentative de connexion pour l'email: {Email}", loginDto.Email);
            throw;
        }
    }

    /// <summary>
    /// Valide un token JWT
    /// </summary>
    /// <returns>True si le token est valide</returns>
    [HttpPost("validate-token")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<bool>> ValidateToken()
    {
        try
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedException("Token non fourni ou format invalide");
            }

            var token = authHeader.Substring("Bearer ".Length);
            var isValid = await _authService.ValidateTokenAsync(token);

            return Ok(isValid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la validation du token");
            throw;
        }
    }

    /// <summary>
    /// Récupère les informations de l'utilisateur à partir du token
    /// </summary>
    /// <returns>Les informations de l'utilisateur</returns>
    [HttpGet("user-info")]
    [ProducesResponseType(typeof(UtilisateurDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UtilisateurDto>> GetUserInfo()
    {
        try
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                throw new UnauthorizedException("Token non fourni ou format invalide");
            }

            var token = authHeader.Substring("Bearer ".Length);
            var user = await _authService.GetUserFromTokenAsync(token);

            if (user == null)
            {
                throw new UnauthorizedException("Utilisateur non trouvé ou token invalide");
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des informations utilisateur");
            throw;
        }
    }
}