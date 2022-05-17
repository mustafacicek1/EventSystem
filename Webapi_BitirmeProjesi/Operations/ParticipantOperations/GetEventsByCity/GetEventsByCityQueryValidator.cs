using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEventsByCity
{
    public class GetEventsByCityQueryValidator:AbstractValidator<GetEventsByCityQuery>
    {
        public GetEventsByCityQueryValidator()
        {
            RuleFor(query => query.CityId).GreaterThan(0);
        }
    }
}
