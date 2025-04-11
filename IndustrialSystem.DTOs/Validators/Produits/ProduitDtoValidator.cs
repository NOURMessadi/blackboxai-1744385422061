using FluentValidation;
using IndustrialSystem.DTOs.Produits;

namespace IndustrialSystem.DTOs.Validators.Produits;

public class ProduitDtoValidator : AbstractValidator<ProduitDto>
{
    public ProduitDtoValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La description ne peut pas dépasser 500 caractères");

        RuleFor(x => x.TypeProduitId)
            .NotEmpty().WithMessage("Le type de produit est requis")
            .GreaterThan(0).WithMessage("Le type de produit doit être valide");

        RuleFor(x => x.FamilleProduitId)
            .NotEmpty().WithMessage("La famille de produit est requise")
            .GreaterThan(0).WithMessage("La famille de produit doit être valide");

        RuleFor(x => x.Reference)
            .NotEmpty().WithMessage("La référence est requise")
            .MaximumLength(50).WithMessage("La référence ne peut pas dépasser 50 caractères")
            .Matches(@"^[A-Z0-9-]+$").WithMessage("La référence doit contenir uniquement des lettres majuscules, des chiffres et des tirets");

        RuleFor(x => x.CodeBarre)
            .MaximumLength(50).WithMessage("Le code-barres ne peut pas dépasser 50 caractères")
            .Matches(@"^[0-9]*$").When(x => !string.IsNullOrEmpty(x.CodeBarre))
            .WithMessage("Le code-barres doit contenir uniquement des chiffres");

        RuleFor(x => x.UniteStockage)
            .NotEmpty().WithMessage("L'unité de stockage est requise")
            .MaximumLength(10).WithMessage("L'unité de stockage ne peut pas dépasser 10 caractères");
    }
}