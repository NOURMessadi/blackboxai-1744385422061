using System.ComponentModel.DataAnnotations;

namespace IndustrialSystem.Data.Entities;

public class Utilisateur
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string MotDePasse { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Matricule { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<HistoriqueAction> HistoriqueActions { get; set; } = new List<HistoriqueAction>();

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}