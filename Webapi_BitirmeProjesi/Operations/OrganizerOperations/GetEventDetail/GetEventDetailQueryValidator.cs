using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetEventDetail
{
    public class GetEventDetailQueryValidator:AbstractValidator<GetEventDetailQuery>
    {
        public GetEventDetailQueryValidator()
        {
            RuleFor(query => query.EventId).GreaterThan(0);
        }
    }
}
