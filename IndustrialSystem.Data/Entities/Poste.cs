using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndustrialSystem.Data.Entities;

public class Poste
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [ForeignKey("TypePoste")]
    public int TypePosteId { get; set; }

    // Navigation properties
    public virtual TypePoste TypePoste { get; set; } = null!;
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    public virtual ICollection<Parametre> Parametres { get; set; } = new List<Parametre>();

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}