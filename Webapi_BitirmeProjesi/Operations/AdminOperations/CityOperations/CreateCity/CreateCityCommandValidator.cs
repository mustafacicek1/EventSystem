using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.CreateCity
{
    public class CreateCityCommandValidator:AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(command => command.Model.CityName).NotEmpty().MinimumLength(2).MaximumLength(30);
        }
    }
}
