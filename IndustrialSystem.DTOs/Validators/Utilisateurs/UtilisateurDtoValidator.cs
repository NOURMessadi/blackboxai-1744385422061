using FluentValidation;
using IndustrialSystem.DTOs.Utilisateurs;

namespace IndustrialSystem.DTOs.Validators.Utilisateurs;

public class UtilisateurDtoValidator : AbstractValidator<UtilisateurDto>
{
    public UtilisateurDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email est requis")
            .EmailAddress().WithMessage("Format d'email invalide")
            .MaximumLength(100).WithMessage("L'email ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Le rôle est requis")
            .MaximumLength(50).WithMessage("Le rôle ne peut pas dépasser 50 caractères")
            .Must(role => role == "Administrateur" || role == "Préparateur" || role == "Paraméteur")
            .WithMessage("Le rôle doit être 'Administrateur', 'Préparateur' ou 'Paraméteur'");

        RuleFor(x => x.Matricule)
            .NotEmpty().WithMessage("Le matricule est requis")
            .MaximumLength(20).WithMessage("Le matricule ne peut pas dépasser 20 caractères")
            .Matches(@"^[A-Z0-9]+$").WithMessage("Le matricule doit contenir uniquement des lettres majuscules et des chiffres");
    }
}