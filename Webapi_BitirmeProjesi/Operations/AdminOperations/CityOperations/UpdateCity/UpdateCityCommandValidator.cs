using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.UpdateCity
{
    public class UpdateCityCommandValidator:AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(command => command.Model.CityName).NotEmpty().MinimumLength(2).MaximumLength(30);
            RuleFor(command => command.CityId).GreaterThan(0);
        }
    }
}
