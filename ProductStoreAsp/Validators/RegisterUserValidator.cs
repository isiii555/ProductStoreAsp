using FluentValidation;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserViewModel>
    {
        public RegisterUserValidator() {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Please input correct email adress").NotEmpty().WithMessage("This field is required!");

            RuleFor(u => u.UserName).MinimumLength(5).MaximumLength(10).WithMessage("Username's length must be between 5 and 10").NotEmpty().WithMessage("This field is required!");

            RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password's length must be more than 6").Equal(t => t.ConfirmPassword).NotEmpty().WithMessage("This field is required!");

            RuleFor(u => u.ConfirmPassword).MinimumLength(6).WithMessage("Password's length must be more than 6").Equal(t => t.Password).NotEmpty().WithMessage("This field is required!");
        }
    }
}
