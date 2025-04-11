using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class Parametre
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = "global"; // "global" ou "détaillé"

    [Required]
    public string Valeur { get; set; } = string.Empty;

    [ForeignKey("Poste")]
    public int? PosteId { get; set; } // Nullable pour les paramètres globaux

    // Navigation property
    public virtual Poste? Poste { get; set; }

    // Métadonnées
    [MaxLength(500)]
    public string? Description { get; set; }
    public string? GroupeParametres { get; set; }
    public string? TypeDonnee { get; set; } // string, number, boolean, etc.
    public string? ValeurParDefaut { get; set; }
    public bool EstModifiable { get; set; } = true
    public bool EstVisible { get; set; } = true;
    public string? Validation { get; set; } // Règles de validation en JSON si nécessaire

    // Audit
    public string? ModifiePar { get; set; }
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}