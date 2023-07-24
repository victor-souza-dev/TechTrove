using Back.Models.Input;
using FluentValidation;

namespace Back.Models.Validator;

public class UserValidatorCreated : AbstractValidator<UserInputCreate>
{
    public UserValidatorCreated()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("Campo obrigatório");
        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Campo obrigatório");
    }
}
public class UserValidatorUpdated : AbstractValidator<UserInputUpdate>
{
    public UserValidatorUpdated()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("Campo obrigatório");
        RuleFor(p => p.Password)
            .NotEmpty()
            .WithMessage("Campo obrigatório");
    }
}
