using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEventDetail
{
    public class GetEventDetailQueryValidator:AbstractValidator<GetEventDetailQuery>
    {
        public GetEventDetailQueryValidator()
        {
            RuleFor(query => query.EventId).GreaterThan(0);
        }
    }
}
