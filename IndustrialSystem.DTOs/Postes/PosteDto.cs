using System.ComponentModel.DataAnnotations;

namespace IndustrialSystem.DTOs.Postes;

public class PosteDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TypePosteId { get; set; }
    public string TypePosteNom { get; set; } = string.Empty;
    public ICollection<ApplicationMinimalDto> Applications { get; set; } = new List<ApplicationMinimalDto>();
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreatePosteDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type de poste est requis")]
    public int TypePosteId { get; set; }
}

public class UpdatePosteDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type de poste est requis")]
    public int TypePosteId { get; set; }
}

public class PosteMinimalDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string TypePosteNom { get; set; } = string.Empty;
}

public class ApplicationMinimalDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
}

public class TypePosteDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public ICollection<PosteMinimalDto> Postes { get; set; } = new List<PosteMinimalDto>();
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateTypePosteDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;
}

public class UpdateTypePosteDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;
}