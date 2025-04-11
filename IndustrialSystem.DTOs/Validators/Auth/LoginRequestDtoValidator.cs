using FluentValidation;
using IndustrialSystem.DTOs.Auth;

namespace IndustrialSystem.DTOs.Validators.Auth;

public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("L'email est requis")
            .EmailAddress().WithMessage("Format d'email invalide")
            .MaximumLength(100).WithMessage("L'email ne peut pas dépasser 100 caractères");

        RuleFor(x => x.MotDePasse)
            .NotEmpty().WithMessage("Le mot de passe est requis")
            .MinimumLength(6).WithMessage("Le mot de passe doit contenir au moins 6 caractères")
            .MaximumLength(100).WithMessage("Le mot de passe ne peut pas dépasser 100 caractères");
    }
}