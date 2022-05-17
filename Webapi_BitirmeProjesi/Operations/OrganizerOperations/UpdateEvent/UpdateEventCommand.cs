using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.Extensions;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.UpdateEvent
{
    public class UpdateEventCommand
    {
        public int EventId { get; set; }
        public UpdateEventModel Model { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public UpdateEventCommand(EventSystemDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
        }

        public void Handle()
        {
            Event updatedEvent = _dbContext.Events.Where(e => e.EventStatus == true).SingleOrDefault(c => c.Id == EventId);
            if (updatedEvent is null)
                throw new InvalidOperationException("Event not found");

            var authMail = _contextAccessor.HttpContext.User.GetEmail();
            var user = _dbContext.Users.SingleOrDefault(u => u.Mail == authMail);
            Organizer organizer = _dbContext.Organizers.SingleOrDefault(o => o.UserId == user.Id);

            if (updatedEvent.OrganizerId != organizer.Id)
                throw new InvalidOperationException("You can't update event which you didn't create");

            if (DateTime.Now.Date.AddDays(5) > updatedEvent.EventDate.Date)
                throw new InvalidOperationException("You can't update this event anymore");

            updatedEvent.Address=Model.Address;
            updatedEvent.MaxParticipant=Model.MaxParticipant;
            _dbContext.SaveChanges();
        }
    }
}
