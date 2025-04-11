using System.ComponentModel.DataAnnotations;
using IndustrialSystem.DTOs.Utilisateurs;

namespace IndustrialSystem.DTOs.Historique;

public class HistoriqueActionDto
{
    public int Id { get; set; }
    public int UtilisateurId { get; set; }
    public string UtilisateurNom { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public DateTime DateAction { get; set; }
    public string? TypeEntite { get; set; }
    public int? EntiteId { get; set; }
    public string? Details { get; set; }
    public string? AncienneValeur { get; set; }
    public string? NouvelleValeur { get; set; }
    public string? AdresseIP { get; set; }
    public string? NavigateurWeb { get; set; }
    public string? Statut { get; set; }
    public string? MessageErreur { get; set; }
    public int? DureeExecution { get; set; }
}

public class CreateHistoriqueActionDto
{
    [Required(ErrorMessage = "L'action est requise")]
    [StringLength(255, ErrorMessage = "L'action ne peut pas dépasser 255 caractères")]
    public string Action { get; set; } = string.Empty;

    [StringLength(100)]
    public string? TypeEntite { get; set; }

    public int? EntiteId { get; set; }

    [StringLength(1000)]
    public string? Details { get; set; }

    public string? AncienneValeur { get; set; }

    public string? NouvelleValeur { get; set; }

    [StringLength(50)]
    public string? AdresseIP { get; set; }

    [StringLength(200)]
    public string? NavigateurWeb { get; set; }

    [StringLength(50)]
    public string? Statut { get; set; }

    [StringLength(500)]
    public string? MessageErreur { get; set; }

    public int? DureeExecution { get; set; }
}

public class RechercheHistoriqueDto
{
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public int? UtilisateurId { get; set; }
    public string? TypeEntite { get; set; }
    public int? EntiteId { get; set; }
    public string? Action { get; set; }
    public string? Statut { get; set; }
    public int Page { get; set; } = 1;
    public int TaillePage { get; set; } = 10;
}

public class StatistiquesHistoriqueDto
{
    public int TotalActions { get; set; }
    public int ActionsReussies { get; set; }
    public int ActionsEchouees { get; set; }
    public Dictionary<string, int> ActionsParType { get; set; } = new();
    public Dictionary<string, int> ActionsParUtilisateur { get; set; } = new();
    public double DureeExecutionMoyenne { get; set; }
}

public class ExportHistoriqueDto
{
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public string? TypeEntite { get; set; }
    public string? Format { get; set; } = "CSV"; // CSV, Excel, PDF
}

public class HistoriqueActionResumeeDto
{
    public int Id { get; set; }
    public string UtilisateurNom { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public DateTime DateAction { get; set; }
    public string? TypeEntite { get; set; }
    public string? Statut { get; set; }
}