using System.ComponentModel.DataAnnotations;

namespace IndustrialSystem.DTOs.Utilisateurs;

public class UtilisateurDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Matricule { get; set; } = string.Empty;
    public DateTime DateCreation { get; set; }
    public DateTime? DateModification { get; set; }
}

public class CreateUtilisateurDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'email est requis")]
    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    [StringLength(100, ErrorMessage = "L'email ne peut pas dépasser 100 caractères")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le mot de passe est requis")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir entre 6 et 100 caractères")]
    public string MotDePasse { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le rôle est requis")]
    [StringLength(50, ErrorMessage = "Le rôle ne peut pas dépasser 50 caractères")]
    public string Role { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le matricule est requis")]
    [StringLength(20, ErrorMessage = "Le matricule ne peut pas dépasser 20 caractères")]
    public string Matricule { get; set; } = string.Empty;
}

public class UpdateUtilisateurDto
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
    public string Nom { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Format d'email invalide")]
    [StringLength(100, ErrorMessage = "L'email ne peut pas dépasser 100 caractères")]
    public string Email { get; set; } = string.Empty;

    [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir entre 6 et 100 caractères")]
    public string? MotDePasse { get; set; }

    [Required(ErrorMessage = "Le rôle est requis")]
    [StringLength(50, ErrorMessage = "Le rôle ne peut pas dépasser 50 caractères")]
    public string Role { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "Le matricule ne peut pas dépasser 20 caractères")]
    public string Matricule { get; set; } = string.Empty;
}

public class UpdatePasswordDto
{
    [Required(ErrorMessage = "L'ancien mot de passe est requis")]
    public string AncienMotDePasse { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nouveau mot de passe est requis")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir entre 6 et 100 caractères")]
    public string NouveauMotDePasse { get; set; } = string.Empty;

    [Required(ErrorMessage = "La confirmation du mot de passe est requise")]
    [Compare("NouveauMotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas")]
    public string ConfirmationMotDePasse { get; set; } = string.Empty;
}