using FluentValidation;
using System;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.CreateEvent
{
    public class CreateEventCommandValidator:AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MaximumLength(50);
            RuleFor(command=>command.Model.EventDate).NotEmpty().GreaterThan(DateTime.Now.AddDays(10));
            RuleFor(command=>command.Model.LastApplicationDate).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(command=>command.Model.LastApplicationDate).LessThan(m=>m.Model.EventDate).When(m=>m.Model.EventDate>DateTime.Now.AddDays(10));
            RuleFor(command => command.Model.Description).NotEmpty().MaximumLength(500);
            RuleFor(command => command.Model.Address).NotEmpty().MaximumLength(250);
            RuleFor(command => command.Model.MaxParticipant).GreaterThan(0);
            RuleFor(command => command.Model.IsItPaid).Must(x => x == false || x == true);
            RuleFor(command => command.Model.TicketPrice).NotEmpty().When(m => m.Model.IsItPaid == true);
            RuleFor(command => command.Model.TicketPrice).GreaterThan(0);
            RuleFor(command => command.Model.CategoryId).GreaterThan(0);
            RuleFor(command => command.Model.CityId).GreaterThan(0);

        }
    }
}
