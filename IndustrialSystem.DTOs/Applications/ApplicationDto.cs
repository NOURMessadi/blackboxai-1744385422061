using System.ComponentModel.DataAnnotations;
using IndustrialSystem.DTOs.Postes;

namespace IndustrialSystem.DTOs.Applications;

public class ApplicationDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public int PosteId { get; set; }
    public PosteMinimalDto Poste { get; set; } = null!;
    public string? CheminInstallation { get; set; }
    public bool EstActive { get; set; }
    public DateTime? DerniereDemarrage { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateApplicationDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "La version est requise")]
    [StringLength(50, ErrorMessage = "La version ne peut pas dépasser 50 caractères")]
    public string Version { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le poste est requis")]
    public int PosteId { get; set; }

    [StringLength(255, ErrorMessage = "Le chemin d'installation ne peut pas dépasser 255 caractères")]
    public string? CheminInstallation { get; set; }

    public bool EstActive { get; set; } = true;
}

public class UpdateApplicationDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "La version est requise")]
    [StringLength(50, ErrorMessage = "La version ne peut pas dépasser 50 caractères")]
    public string Version { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le poste est requis")]
    public int PosteId { get; set; }

    [StringLength(255, ErrorMessage = "Le chemin d'installation ne peut pas dépasser 255 caractères")]
    public string? CheminInstallation { get; set; }

    public bool EstActive { get; set; }
}

public class ApplicationListDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string PosteNom { get; set; } = string.Empty;
    public bool EstActive { get; set; }
    public DateTime? DerniereDemarrage { get; set; }
}

public class ApplicationStatusUpdateDto
{
    public bool EstActive { get; set; }
}

public class ApplicationVersionUpdateDto
{
    [Required(ErrorMessage = "La nouvelle version est requise")]
    [StringLength(50, ErrorMessage = "La version ne peut pas dépasser 50 caractères")]
    public string NouvelleVersion { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Les notes de mise à jour ne peuvent pas dépasser 500 caractères")]
    public string? NotesVersion { get; set; }
}