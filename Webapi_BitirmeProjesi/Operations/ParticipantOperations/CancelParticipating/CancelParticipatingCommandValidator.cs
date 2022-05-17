using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.CancelParticipating
{
    public class CancelParticipatingCommandValidator:AbstractValidator<CancelParticipatingCommand>
    {
        public CancelParticipatingCommandValidator()
        {
            RuleFor(command => command.EventId).GreaterThan(0);
        }
    }
}
