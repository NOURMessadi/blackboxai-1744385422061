using FluentValidation;
using IndustrialSystem.DTOs.Operations;

namespace IndustrialSystem.DTOs.Validators.Operations;

public class OperationDtoValidator : AbstractValidator<OperationDto>
{
    public OperationDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom de l'opération est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La description ne peut pas dépasser 500 caractères");

        RuleFor(x => x.ProduitId)
            .NotEmpty().WithMessage("Le produit associé est requis")
            .GreaterThan(0).WithMessage("L'identifiant du produit doit être valide");

        RuleFor(x => x.OrdreExecution)
            .NotEmpty().WithMessage("L'ordre d'exécution est requis")
            .GreaterThan(0).WithMessage("L'ordre d'exécution doit être supérieur à 0");

        RuleFor(x => x.DureeEstimee)
            .GreaterThanOrEqualTo(0).WithMessage("La durée estimée doit être positive ou nulle")
            .When(x => x.DureeEstimee.HasValue);

        RuleFor(x => x.PosteId)
            .GreaterThan(0).WithMessage("L'identifiant du poste doit être valide")
            .When(x => x.PosteId.HasValue);

        RuleFor(x => x.Instructions)
            .MaximumLength(1000).WithMessage("Les instructions ne peuvent pas dépasser 1000 caractères");

        RuleFor(x => x.ParametresRequis)
            .Must(BeValidJson).WithMessage("Le format JSON des paramètres requis est invalide")
            .When(x => !string.IsNullOrEmpty(x.ParametresRequis));

        RuleFor(x => x.OutilsNecessaires)
            .Must(BeValidJson).WithMessage("Le format JSON des outils nécessaires est invalide")
            .When(x => !string.IsNullOrEmpty(x.OutilsNecessaires));

        RuleFor(x => x.CompetencesRequises)
            .Must(BeValidJson).WithMessage("Le format JSON des compétences requises est invalide")
            .When(x => !string.IsNullOrEmpty(x.CompetencesRequises));
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
}
