using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class Operation
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [ForeignKey("Produit")]
    public int ProduitId { get; set; }

    // Navigation property
    public virtual Produit Produit { get; set; } = null!;

    // Métadonnées opérationnelles
    public int OrdreExecution { get; set; }
    public decimal? DureeEstimee { get; set; }
    public string? UniteTemps { get; set; } = "minutes";
    public bool EstObligatoire { get; set; } = true;
    public string? Instructions { get; set; }
    public string? Ressources { get; set; }
    public bool EstActive { get; set; } = true;

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}