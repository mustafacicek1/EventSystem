using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.UserLogin
{
    public class UserLoginCommandValidator:AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(command => command.Model.Mail).NotEmpty();
            RuleFor(command => command.Model.Password).NotEmpty();
        }
    }
}
