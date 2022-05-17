using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.CancelEvent
{
    public class CancelEventCommandValidator:AbstractValidator<CancelEventCommand>
    {
        public CancelEventCommandValidator()
        {
            RuleFor(command => command.EventId).GreaterThan(0);
        }
    }
}
