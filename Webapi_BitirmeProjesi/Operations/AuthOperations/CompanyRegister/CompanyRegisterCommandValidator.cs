using FluentValidation;
using System.Text.RegularExpressions;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.CompanyRegister
{
    public class CompanyRegisterCommandValidator:AbstractValidator<CompanyRegisterCommand>
    {
        public CompanyRegisterCommandValidator()
        {
            RuleFor(command => command.Model.CompanyName).NotEmpty().MinimumLength(2).MaximumLength(30);
            RuleFor(command => command.Model.Domain).NotEmpty().MaximumLength(100);
            RuleFor(command => command.Model.Mail).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(8).MaximumLength(30).Must(x => IsValid(x)).WithMessage("Password has to contains 1  number and 1 letter");

        }

        private bool IsValid(string pw)
        {
            var letter = new Regex("[a-zA-Z]+");
            var digit = new Regex("(\\d)+");

            return letter.IsMatch(pw) && digit.IsMatch(pw);
        }
    }
}
