using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class HistoriqueAction
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Utilisateur")]
    public int UtilisateurId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Action { get; set; } = string.Empty;

    [Required]
    public DateTime DateAction { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual Utilisateur Utilisateur { get; set; } = null!;

    // Détails supplémentaires
    public string? TypeEntite { get; set; }
    public int? EntiteId { get; set; }
    public string? Details { get; set; }
    public string? AncienneValeur { get; set; }
    public string? NouvelleValeur { get; set; }
    public string? AdresseIP { get; set; }
    public string? NavigateurWeb { get; set; }

    // Métadonnées
    public string? Statut { get; set; } // Succès, Échec, etc.
    public string? MessageErreur { get; set; }
    public int? DureeExecution { get; set; } // en millisecondes
}