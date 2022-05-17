using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.Extensions;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.JoinEvent
{
    public class JoinEventCommand
    {
        public int EventId { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public JoinEventCommand(EventSystemDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _contextAccessor = contextAccessor;
        }

        public List<CompaniesViewModel> Handle()
        {
            Event evnt = _dbContext.Events.Where(e => e.EventStatus == true).SingleOrDefault(c => c.Id == EventId);
            if (evnt is null)
                throw new InvalidOperationException("Event not found");

            var authMail = _contextAccessor.HttpContext.User.GetEmail();
            var user = _dbContext.Users.SingleOrDefault(u => u.Mail == authMail);
            Participant participant = _dbContext.Participants.SingleOrDefault(o => o.UserId == user.Id);

            var eventParticipant = _dbContext.EventParticipants.SingleOrDefault(x => x.EventId == EventId && x.ParticipantId == participant.Id);
            if (eventParticipant is not null)
                throw new InvalidOperationException("You already set participation status for this event");

            if (DateTime.Now > evnt.LastApplicationDate)
                throw new InvalidOperationException("Last application date is passed");

            int evntParticipantCount = evnt.EventParticipants.Where(x => x.ParticipationStatus == true).Count();
            if (evnt.MaxParticipant == evntParticipantCount)
                throw new InvalidOperationException("Participant count is reached maximum count");

            List<CompaniesViewModel> companies = new List<CompaniesViewModel>();
            if (evnt.IsItPaid == true)
            {
                companies = _dbContext.Companies.Select(c=>new CompaniesViewModel
                {
                   CompanyName=c.CompanyName,
                   Domain=c.Domain,
                }).ToList();
            }

            EventParticipant newEventParticipant = new EventParticipant();
            newEventParticipant.ParticipantId = participant.Id;
            newEventParticipant.EventId = EventId;
            newEventParticipant.ParticipationStatus = true;
            _dbContext.EventParticipants.Add(newEventParticipant);
            _dbContext.SaveChanges();
            return companies;
        }
    }
}
