using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEventsByCategory
{
    public class GetEventsByCategoryQueryValidator:AbstractValidator<GetEventsByCategoryQuery>
    {
        public GetEventsByCategoryQueryValidator()
        {
            RuleFor(query => query.CategoryId).GreaterThan(0);
        }
    }
}
