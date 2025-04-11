using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class Produit
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [ForeignKey("TypeProduit")]
    public int TypeProduitId { get; set; }

    [Required]
    [ForeignKey("FamilleProduit")]
    public int FamilleProduitId { get; set; }

    // Navigation properties
    public virtual TypeProduit TypeProduit { get; set; } = null!;
    public virtual FamilleProduit FamilleProduit { get; set; } = null!;
    public virtual ICollection<Operation> Operations { get; set; } = new List<Operation>();
    public virtual ICollection<Nomenclature> Nomenclatures { get; set; } = new List<Nomenclature>();

    // Métadonnées
    public string? CodeProduit { get; set; }
    public string? Reference { get; set; }
    public bool EstActif { get; set; } = true;
    public decimal? PrixUnitaire { get; set; }
    public string? Unite { get; set; }

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}