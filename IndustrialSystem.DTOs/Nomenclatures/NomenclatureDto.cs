using System.ComponentModel.DataAnnotations;
using IndustrialSystem.DTOs.Produits;

namespace IndustrialSystem.DTOs.Nomenclatures;

public class NomenclatureDto
{
    public int Id { get; set; }
    public int ProduitId { get; set; }
    public ProduitMinimalDto ProduitFini { get; set; } = null!;
    public List<ComposantNomenclatureDto> Composants { get; set; } = new();
    public string? Version { get; set; }
    public bool EstActive { get; set; }
    public string? Notes { get; set; }
    public bool EstValidee { get; set; }
    public DateTime? DateValidation { get; set; }
    public string? ValidePar { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class ComposantNomenclatureDto
{
    public int ProduitId { get; set; }
    public ProduitMinimalDto Produit { get; set; } = null!;
    public decimal Quantite { get; set; }
    public string? Unite { get; set; }
    public string? Notes { get; set; }
}

public class CreateNomenclatureDto
{
    [Required(ErrorMessage = "Le produit fini est requis")]
    public int ProduitId { get; set; }

    [Required(ErrorMessage = "La liste des composants est requise")]
    public List<CreateComposantNomenclatureDto> Composants { get; set; } = new();

    [StringLength(50)]
    public string? Version { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }
}

public class CreateComposantNomenclatureDto
{
    [Required(ErrorMessage = "Le produit composant est requis")]
    public int ProduitId { get; set; }

    [Required(ErrorMessage = "La quantité est requise")]
    [Range(0.0001, double.MaxValue, ErrorMessage = "La quantité doit être supérieure à 0")]
    public decimal Quantite { get; set; }

    [StringLength(20)]
    public string? Unite { get; set; }

    [StringLength(200)]
    public string? Notes { get; set; }
}

public class UpdateNomenclatureDto
{
    [Required(ErrorMessage = "La liste des composants est requise")]
    public List<CreateComposantNomenclatureDto> Composants { get; set; } = new();

    [StringLength(50)]
    public string? Version { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    public bool EstActive { get; set; }
}

public class ValidateNomenclatureDto
{
    public bool EstValidee { get; set; }
    
    [StringLength(200)]
    public string? CommentaireValidation { get; set; }
}

public class NomenclatureMinimalDto
{
    public int Id { get; set; }
    public string ProduitFiniNom { get; set; } = string.Empty;
    public string? Version { get; set; }
    public bool EstActive { get; set; }
    public bool EstValidee { get; set; }
    public int NombreComposants { get; set; }
}

public class CopieNomenclatureDto
{
    [Required(ErrorMessage = "Le produit fini cible est requis")]
    public int NouveauProduitId { get; set; }

    [StringLength(50)]
    public string? NouvelleVersion { get; set; }
}