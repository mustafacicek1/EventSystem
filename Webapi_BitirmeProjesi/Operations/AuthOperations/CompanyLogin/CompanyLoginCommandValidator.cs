using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.CompanyLogin
{
    public class CompanyLoginCommandValidator:AbstractValidator<CompanyLoginCommand>
    {
        public CompanyLoginCommandValidator()
        {
            RuleFor(command => command.Model.Mail).NotEmpty();
            RuleFor(command => command.Model.Password).NotEmpty();
        }
    }
}
