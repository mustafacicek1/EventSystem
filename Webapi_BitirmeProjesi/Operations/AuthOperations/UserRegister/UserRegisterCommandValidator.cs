using FluentValidation;
using System;
using System.Text.RegularExpressions;
using Webapi_BitirmeProjesi.Common;

namespace Webapi_BitirmeProjesi.Operations.AuthOperations.UserRegister
{
    public class UserRegisterCommandValidator:AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(command=>command.Model.FirstName).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(command=>command.Model.LastName).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(command=>command.Model.Mail).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(command=>command.Model.RoleId).NotEmpty().InclusiveBetween(1,2).WithMessage("RoleId must be 1(organizer) or 2(participant)");
            RuleFor(command=>command.Model.Password).NotEmpty().MinimumLength(8).MaximumLength(30).Must(x=>IsValid(x)).WithMessage("Password has to contains 1  number and 1 letter");
            RuleFor(command => command.Model.ConfirmPassword).Equal(x=>x.Model.Password).WithMessage("Passwords must match");


        }

        private bool IsValid(string pw)
        {
            var letter = new Regex("[a-zA-Z]+");
            var digit = new Regex("(\\d)+");

            return letter.IsMatch(pw) && digit.IsMatch(pw);
        }
    }
}
