using System.ComponentModel.DataAnnotations;

namespace IndustrialSystem.DTOs.Produits;

public class ProduitDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TypeProduitId { get; set; }
    public string TypeProduitNom { get; set; } = string.Empty;
    public int FamilleProduitId { get; set; }
    public string FamilleProduitNom { get; set; } = string.Empty;
    public string? CodeProduit { get; set; }
    public string? Reference { get; set; }
    public bool EstActif { get; set; }
    public decimal? PrixUnitaire { get; set; }
    public string? Unite { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateProduitDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type de produit est requis")]
    public int TypeProduitId { get; set; }

    [Required(ErrorMessage = "La famille de produit est requise")]
    public int FamilleProduitId { get; set; }

    [StringLength(50)]
    public string? CodeProduit { get; set; }

    [StringLength(50)]
    public string? Reference { get; set; }

    public decimal? PrixUnitaire { get; set; }

    [StringLength(20)]
    public string? Unite { get; set; }
}

public class UpdateProduitDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type de produit est requis")]
    public int TypeProduitId { get; set; }

    [Required(ErrorMessage = "La famille de produit est requise")]
    public int FamilleProduitId { get; set; }

    [StringLength(50)]
    public string? CodeProduit { get; set; }

    [StringLength(50)]
    public string? Reference { get; set; }

    public bool EstActif { get; set; }

    public decimal? PrixUnitaire { get; set; }

    [StringLength(20)]
    public string? Unite { get; set; }
}

public class ProduitMinimalDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? CodeProduit { get; set; }
    public string TypeProduitNom { get; set; } = string.Empty;
    public string FamilleProduitNom { get; set; } = string.Empty;
}

public class TypeProduitDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? CodeInterne { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateTypeProduitDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? CodeInterne { get; set; }
}

public class UpdateTypeProduitDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? CodeInterne { get; set; }
}

public class FamilleProduitDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? CodeFamille { get; set; }
    public bool EstActive { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateFamilleProduitDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? CodeFamille { get; set; }
}

public class UpdateFamilleProduitDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? CodeFamille { get; set; }

    public bool EstActive { get; set; }
}