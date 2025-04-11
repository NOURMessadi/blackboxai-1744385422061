using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class Nomenclature
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("ProduitFini")]
    public int ProduitId { get; set; }

    // Navigation property pour le produit fini
    public virtual Produit ProduitFini { get; set; } = null!;

    // Composants et quantités stockés en JSON
    [Required]
    public string Composants { get; set; } = "[]"; // Format: [{"produitId": 1, "quantite": 2}, ...]

    // Métadonnées
    public string? Version { get; set; }
    public bool EstActive { get; set; } = true;
    public string? Notes { get; set; }
    
    // Validation et approbation
    public bool EstValidee { get; set; }
    public DateTime? DateValidation { get; set; }
    public string? ValidePar { get; set; }

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }

    [NotMapped]
    public List<ComposantNomenclature> ComposantsList 
    {
        get => System.Text.Json.JsonSerializer.Deserialize<List<ComposantNomenclature>>(Composants) ?? new();
        set => Composants = System.Text.Json.JsonSerializer.Serialize(value);
    }
}

public class ComposantNomenclature
{
    public int ProduitId { get; set; }
    public decimal Quantite { get; set; }
    public string? Unite { get; set; }
    public string? Notes { get; set; }
}