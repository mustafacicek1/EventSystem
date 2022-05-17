using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.JoinEvent
{
    public class JoinEventCommandValidator:AbstractValidator<JoinEventCommand>
    {
        public JoinEventCommandValidator()
        {
            RuleFor(command => command.EventId).GreaterThan(0);
        }
    }
}
