using FluentValidation;
using IndustrialSystem.DTOs.Nomenclatures;
using System.Text.Json;

namespace IndustrialSystem.DTOs.Validators.Nomenclatures;

public class NomenclatureDtoValidator : AbstractValidator<NomenclatureDto>
{
    public NomenclatureDtoValidator()
    {
        RuleFor(x => x.ProduitId)
            .NotEmpty().WithMessage("Le produit fini est requis")
            .GreaterThan(0).WithMessage("L'identifiant du produit fini doit être valide");

        RuleFor(x => x.Composants)
            .NotEmpty().WithMessage("La liste des composants est requise")
            .Must(BeValidJson).WithMessage("Le format JSON des composants est invalide")
            .Must(ContainValidComponents).WithMessage("Les composants doivent avoir un ID de produit et une quantité valide");

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("La version est requise")
            .MaximumLength(20).WithMessage("La version ne peut pas dépasser 20 caractères")
            .Matches(@"^[0-9\.]+$").WithMessage("La version doit être au format numérique (ex: 1.0.0)");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Les notes ne peuvent pas dépasser 500 caractères");

        RuleFor(x => x.EstValidee)
            .NotNull().WithMessage("Le statut de validation est requis");

        When(x => x.EstValidee, () => {
            RuleFor(x => x.ValidePar)
                .NotEmpty().WithMessage("Le validateur est requis lorsque la nomenclature est validée")
                .MaximumLength(100).WithMessage("Le nom du validateur ne peut pas dépasser 100 caractères");

            RuleFor(x => x.DateValidation)
                .NotNull().WithMessage("La date de validation est requise lorsque la nomenclature est validée")
                .Must(date => date <= DateTime.UtcNow)
                .WithMessage("La date de validation ne peut pas être dans le futur");
        });
    }

    private bool BeValidJson(string json)
    {
        if (string.IsNullOrEmpty(json)) return false;
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool ContainValidComponents(string json)
    {
        if (string.IsNullOrEmpty(json)) return false;
        try
        {
            var components = JsonDocument.Parse(json).RootElement;
            if (!components.EnumerateArray().Any()) return false;

            foreach (var component in components.EnumerateArray())
            {
                if (!component.TryGetProperty("produitId", out var produitId) ||
                    !component.TryGetProperty("quantite", out var quantite) ||
                    !component.TryGetProperty("unite", out var unite) ||
                    produitId.GetInt32() <= 0 ||
                    quantite.GetDecimal() <= 0 ||
                    string.IsNullOrEmpty(unite.GetString()))
                {
                    return false;
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}