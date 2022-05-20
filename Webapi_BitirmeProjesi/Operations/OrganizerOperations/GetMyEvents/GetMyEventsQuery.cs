using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using Webapi_BitirmeProjesi.Entities;
using Webapi_BitirmeProjesi.Extensions;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetMyEvents
{
    public class GetMyEventsQuery
    {
        private readonly EventSystemDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMyEventsQuery(EventSystemDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<EventsViewModel> Handle()
        {
            var authMail = _httpContextAccessor.HttpContext.User.GetEmail();
            var authUser = _dbContext.Users.SingleOrDefault(u => u.Mail == authMail);
            Organizer authOrganizer = _dbContext.Organizers.SingleOrDefault(o => o.UserId == authUser.Id);

            List<EventsViewModel> events = (from ev in _dbContext.Events
                                            join category in _dbContext.Categories
                                            on ev.CategoryId equals category.Id
                                            join city in _dbContext.Cities
                                            on ev.CityId equals city.Id
                                            join organizer in _dbContext.Organizers
                                            on ev.OrganizerId equals organizer.Id
                                            join user in _dbContext.Users
                                            on organizer.UserId equals user.Id
                                            where ev.OrganizerId ==authOrganizer.Id
                                            &&ev.EventStatus==true
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
