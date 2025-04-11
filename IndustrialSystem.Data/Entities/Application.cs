using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class Application
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Version { get; set; } = string.Empty;

    [Required]
    [ForeignKey("Poste")]
    public int PosteId { get; set; }

    // Navigation property
    public virtual Poste Poste { get; set; } = null!;

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }

    // Métadonnées supplémentaires
    public string? CheminInstallation { get; set; }
    public bool EstActive { get; set; } = true;
    public DateTime? DerniereDemarrage { get; set; }
}