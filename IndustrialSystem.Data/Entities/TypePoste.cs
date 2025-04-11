using System.ComponentModel.DataAnnotations;

namespace IndustrialSystem.Data.Entities;

public class TypePoste
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nom { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Poste> Postes { get; set; } = new List<Poste>();

    // Timestamps
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime? DateModification { get; set; }
}