using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.AdminOperations.CityOperations.DeleteCity
{
    public class DeleteCityCommandValidator:AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(command => command.CityId).GreaterThan(0);
        }
    }
}
