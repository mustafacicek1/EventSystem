using Webapi_BitirmeProjesi.DbOperations;
using Webapi_BitirmeProjesi.DTOs;
using System.Linq;

namespace Webapi_BitirmeProjesi.Operations.OrganizerOperations.GetEventDetail
{
    public class GetEventDetailQuery
    {
        public int EventId { get; set; }
        private readonly EventSystemDbContext _dbContext;

        public GetEventDetailQuery(EventSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EventsViewModel Handle()
        {
            EventsViewModel evnt = (from ev in _dbContext.Events
                                            join category in _dbContext.Categories
                                            on ev.CategoryId equals category.Id
                                            join city in _dbContext.Cities
                                            on ev.CityId equals city.Id
                                            join organizer in _dbContext.Organizers
                                            on ev.OrganizerId equals organizer.Id
                                            join user in _dbContext.Users
                                            on organizer.UserId equals user.Id
                                            where ev.EventStatus == true
                                            && ev.Id==EventId
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
                                            }).SingleOrDefault();

            return evnt;
        }
    }
}
