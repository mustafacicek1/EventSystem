using FluentValidation;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.UpdateEvent
{
    public class UpdateEventCommandValidator:AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(command => command.Model.MaxParticipant).GreaterThan(0);
            RuleFor(command => command.Model.Address).NotEmpty().MaximumLength(250);
            RuleFor(command => command.EventId).GreaterThan(0);
        }
    }
}
