using FluentValidation;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserViewModel>
    {
        public LoginUserValidator() {

            RuleFor(u => u.Email).EmailAddress().WithMessage("Please input correct email adress").NotEmpty().WithMessage("This field is required!");

            RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password's length must be more than 6").NotEmpty().WithMessage("This field is required!");
        }
    }
}
