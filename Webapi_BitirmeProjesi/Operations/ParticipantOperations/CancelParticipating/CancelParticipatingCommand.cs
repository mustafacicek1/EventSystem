using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.Extensions;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.CancelParticipating
{
    public class CancelParticipatingCommand
    {
        public int EventId { get; set; }
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public CancelParticipatingCommand(EventSystemDbContext dbContext, IHttpContextAccessor contextAccessor)
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

            var eventParticipant = _dbContext.EventParticipants
                .SingleOrDefault(x => x.EventId == EventId && x.ParticipantId == participant.Id &&x.ParticipationStatus==true);

            if (eventParticipant is null)
                throw new InvalidOperationException("You already didn't join this event");

            if (DateTime.Now.Date.AddDays(2) > evnt.EventDate.Date)
                throw new InvalidOperationException("You can't change participation status anymore for this event");

            List<CompaniesViewModel> companies = new List<CompaniesViewModel>();
            if (evnt.IsItPaid == true)
            {
                companies = _dbContext.Companies.Select(c => new CompaniesViewModel
                {
                    CompanyName = c.CompanyName,
                    Domain = c.Domain,
                }).ToList();
            }

            eventParticipant.ParticipationStatus = false;
            _dbContext.SaveChanges();
            return companies;
        }
    }
}
