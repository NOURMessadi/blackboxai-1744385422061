using System.ComponentModel.DataAnnotations;
using IndustrialSystem.DTOs.Produits;

namespace IndustrialSystem.DTOs.Operations;

public class OperationDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ProduitId { get; set; }
    public ProduitMinimalDto Produit { get; set; } = null!;
    public int OrdreExecution { get; set; }
    public decimal? DureeEstimee { get; set; }
    public string? UniteTemps { get; set; }
    public bool EstObligatoire { get; set; }
    public string? Instructions { get; set; }
    public string? Ressources { get; set; }
    public bool EstActive { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateOperationDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le produit est requis")]
    public int ProduitId { get; set; }

    [Required(ErrorMessage = "L'ordre d'exécution est requis")]
    [Range(1, int.MaxValue, ErrorMessage = "L'ordre d'exécution doit être supérieur à 0")]
    public int OrdreExecution { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "La durée estimée doit être positive")]
    public decimal? DureeEstimee { get; set; }

    [StringLength(20)]
    public string? UniteTemps { get; set; } = "minutes";

    public bool EstObligatoire { get; set; } = true;

    [StringLength(1000)]
    public string? Instructions { get; set; }

    [StringLength(500)]
    public string? Ressources { get; set; }
}

public class UpdateOperationDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'ordre d'exécution est requis")]
    [Range(1, int.MaxValue, ErrorMessage = "L'ordre d'exécution doit être supérieur à 0")]
    public int OrdreExecution { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "La durée estimée doit être positive")]
    public decimal? DureeEstimee { get; set; }

    [StringLength(20)]
    public string? UniteTemps { get; set; }

    public bool EstObligatoire { get; set; }

    [StringLength(1000)]
    public string? Instructions { get; set; }

    [StringLength(500)]
    public string? Ressources { get; set; }

    public bool EstActive { get; set; }
}

public class OperationMinimalDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public int OrdreExecution { get; set; }
    public bool EstObligatoire { get; set; }
    public bool EstActive { get; set; }
}

public class ReordonnerOperationsDto
{
    [Required(ErrorMessage = "La liste des opérations est requise")]
    public List<OperationOrdreDto> Operations { get; set; } = new();
}

public class OperationOrdreDto
{
    public int OperationId { get; set; }
    public int NouvelOrdre { get; set; }
}