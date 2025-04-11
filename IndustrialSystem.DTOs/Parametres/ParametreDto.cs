using System.ComponentModel.DataAnnotations;
using IndustrialSystem.DTOs.Postes;

namespace IndustrialSystem.DTOs.Parametres;

public class ParametreDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Valeur { get; set; } = string.Empty;
    public int? PosteId { get; set; }
    public PosteMinimalDto? Poste { get; set; }
    public string? Description { get; set; }
    public string? GroupeParametres { get; set; }
    public string? TypeDonnee { get; set; }
    public string? ValeurParDefaut { get; set; }
    public bool EstModifiable { get; set; }
    public bool EstVisible { get; set; }
    public string? Validation { get; set; }
    public string? ModifiePar { get; set; }
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateParametreDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le type est requis")]
    [StringLength(50, ErrorMessage = "Le type ne peut pas dépasser 50 caractères")]
    public string Type { get; set; } = "global";

    [Required(ErrorMessage = "La valeur est requise")]
    public string Valeur { get; set; } = string.Empty;

    public int? PosteId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? GroupeParametres { get; set; }

    [StringLength(50)]
    public string? TypeDonnee { get; set; }

    public string? ValeurParDefaut { get; set; }

    public bool EstModifiable { get; set; } = true;

    public bool EstVisible { get; set; } = true;

    [StringLength(500)]
    public string? Validation { get; set; }
}

public class UpdateParametreDto
{
    [Required(ErrorMessage = "La valeur est requise")]
    public string Valeur { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public bool EstModifiable { get; set; }

    public bool EstVisible { get; set; }

    [StringLength(500)]
    public string? Validation { get; set; }
}

public class ParametreMinimalDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Valeur { get; set; } = string.Empty;
    public string? PosteNom { get; set; }
}

public class UpdateParametresGroupeDto
{
    [Required(ErrorMessage = "Le groupe de paramètres est requis")]
    public string GroupeParametres { get; set; } = string.Empty;

    [Required(ErrorMessage = "La liste des paramètres est requise")]
    public List<ParametreValeurDto> Parametres { get; set; } = new();
}

public class ParametreValeurDto
{
    public int ParametreId { get; set; }
    public string Valeur { get; set; } = string.Empty;
}

public class ImportParametresDto
{
    [Required(ErrorMessage = "Le fichier de paramètres est requis")]
    public string ContenuFichier { get; set; } = string.Empty;

    public bool EcraserExistants { get; set; } = false;
}

public class ExportParametresDto
{
    public string? GroupeParametres { get; set; }
    public string? Type { get; set; }
    public int? PosteId { get; set; }
}