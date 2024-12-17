using FluentValidation;
using TicketsFilm.Service.Controllers.Users.Entities;

namespace TicketsFilm.Service.Controllers.Validation;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200)
            .WithMessage("UserName is required");
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Name is required");
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role is required");
        RuleFor(x => x.Numberphone)
            .NotEmpty()
            .Matches("+")
            .WithMessage("Numberphone is required");
        RuleFor(x => x.Age)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Age is required");
        
    }
}