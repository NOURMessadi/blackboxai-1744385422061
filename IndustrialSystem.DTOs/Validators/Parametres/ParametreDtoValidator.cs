using FluentValidation;
using IndustrialSystem.DTOs.Parametres;

namespace IndustrialSystem.DTOs.Validators.Parametres;

public class ParametreDtoValidator : AbstractValidator<ParametreDto>
{
    public ParametreDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom du paramètre est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Le type de paramètre est requis")
            .Must(type => type == "global" || type == "détaillé")
            .WithMessage("Le type doit être 'global' ou 'détaillé'");

        RuleFor(x => x.Valeur)
            .NotEmpty().WithMessage("La valeur du paramètre est requise")
            .MaximumLength(500).WithMessage("La valeur ne peut pas dépasser 500 caractères");

        RuleFor(x => x.PosteId)
            .NotNull().When(x => x.Type == "détaillé")
            .WithMessage("L'ID du poste est requis pour un paramètre détaillé")
            .Null().When(x => x.Type == "global")
            .WithMessage("L'ID du poste doit être null pour un paramètre global");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La description ne peut pas dépasser 500 caractères");

        RuleFor(x => x.TypeDonnee)
            .NotEmpty().WithMessage("Le type de donnée est requis")
            .Must(type => type == "texte" || type == "nombre" || type == "booléen" || type == "date")
            .WithMessage("Le type de donnée doit être 'texte', 'nombre', 'booléen' ou 'date'");

        RuleFor(x => x.ValeurParDefaut)
            .MaximumLength(500).WithMessage("La valeur par défaut ne peut pas dépasser 500 caractères");

        RuleFor(x => x.EstObligatoire)
            .NotNull().WithMessage("Le champ EstObligatoire est requis");

        RuleFor(x => x.Validation)
            .Must(BeValidJson).When(x => !string.IsNullOrEmpty(x.Validation))
            .WithMessage("Le format JSON des règles de validation est invalide");

        When(x => x.TypeDonnee == "nombre", () => {
            RuleFor(x => x.Valeur)
                .Matches(@"^-?\d*\.?\d+$")
                .WithMessage("La valeur doit être un nombre valide");
        });

        When(x => x.TypeDonnee == "booléen", () => {
            RuleFor(x => x.Valeur)
                .Must(v => v.ToLower() == "true" || v.ToLower() == "false")
                .WithMessage("La valeur doit être 'true' ou 'false'");
        });

        When(x => x.TypeDonnee == "date", () => {
            RuleFor(x => x.Valeur)
                .Must(BeValidDate)
                .WithMessage("La valeur doit être une date valide au format ISO 8601");
        });
    }

    private bool BeValidJson(string json)
    {
        if (string.IsNullOrEmpty(json)) return true;
        try
        {
            System.Text.Json.JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool BeValidDate(string date)
    {
        if (string.IsNullOrEmpty(date)) return false;
        return DateTime.TryParse(date, out _);
    }
}
