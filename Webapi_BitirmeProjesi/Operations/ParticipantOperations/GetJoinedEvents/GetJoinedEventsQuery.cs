using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using System.Linq;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.Extensions;
using System;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetJoinedEvents
{
    public class GetJoinedEventsQuery
    {
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetJoinedEventsQuery(EventSystemDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }

        public List<EventsViewModel> Handle()
        {
            var authMail = _contextAccessor.HttpContext.User.GetEmail();
            var authUser = _dbContext.Users.SingleOrDefault(u => u.Mail == authMail);
            Participant participant = _dbContext.Participants.SingleOrDefault(o => o.UserId == authUser.Id);

            List<EventsViewModel> events = (from ev in _dbContext.Events
                                            join category in _dbContext.Categories
                                            on ev.CategoryId equals category.Id
                                            join city in _dbContext.Cities
                                            on ev.CityId equals city.Id
                                            join organizer in _dbContext.Organizers
                                            on ev.OrganizerId equals organizer.Id
                                            join user in _dbContext.Users
                                            on organizer.UserId equals user.Id
                                            where ev.EventStatus == true
                                            && ev.EventParticipants.Where(x=>x.ParticipantId==participant.Id &&
                                            x.ParticipationStatus==true).Any()
                                            select new EventsViewModel
                                            {
                                                EventId = ev.Id,
                                                EventName = ev.Name,
                                                EventDate = ev.EventDate,
                                                LastApplicationDate = ev.LastApplicationDate,
                                                OrganizerName = user.FirstName + " " + user.LastName,
                                                City = city.Name,
                                                Category = category.Name,
                                                Description = ev.Description,
                                                Address = ev.Address,
                                                MaxParticipant = ev.MaxParticipant,
                                                ParticipantCount = ev.EventParticipants.Where(x => x.ParticipationStatus == true).Count(),
                                                TicketPrice = ev.TicketPrice
                                            }).ToList();

            return events;
        }
    }
}
