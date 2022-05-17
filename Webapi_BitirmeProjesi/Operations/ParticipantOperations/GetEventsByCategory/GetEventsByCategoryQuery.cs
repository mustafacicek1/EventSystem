using System.Collections.Generic;
using Webapi_BitirmeProjesi.DbOperations;
using System.Linq;
using Webapi_BitirmeProjesi.Entities;
using System;
using Webapi_BitirmeProjesi.DTOs;

namespace Webapi_BitirmeProjesi.Operations.ParticipantOperations.GetEventsByCategory
{
    public class GetEventsByCategoryQuery
    {
        public int CategoryId { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public GetEventsByCategoryQuery(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EventsViewModel> Handle()
        {
            Category ctg = _dbContext.Categories.SingleOrDefault(c=>c.Id == CategoryId);
            if (ctg is null)
                throw new InvalidOperationException("Category not found");

            List<EventsViewModel> events = (from ev in _dbContext.Events
                                            join category in _dbContext.Categories
                                            on ev.CategoryId equals category.Id
                                            join city in _dbContext.Cities
                                            on ev.CityId equals city.Id
                                            join organizer in _dbContext.Organizers
                                            on ev.OrganizerId equals organizer.Id
                                            join user in _dbContext.Users
                                            on organizer.UserId equals user.Id
                                            where ev.EventStatus == true && ev.CategoryId==CategoryId
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
