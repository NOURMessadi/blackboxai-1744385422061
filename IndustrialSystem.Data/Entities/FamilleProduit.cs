using System.ComponentModel.DataAnnotations;

namespace IndustrialSystem.Data.Entities;

public class FamilleProduit
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();

    // Métadonnées
    public string? Description { get; set; }
    public string? CodeFamille { get; set; }
    public bool EstActive { get; set; } = true;

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}