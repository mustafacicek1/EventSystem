using System;
using System.Collections.Generic;

namespace Webapi_BitirmeProjesi.Entities
{
    public class Event
    {
        public Event()
        {
            EventParticipants=new HashSet<EventParticipant>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MaxParticipant { get; set; }
        public bool IsItPaid { get; set; }
        public decimal? TicketPrice { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public int OrganizerId { get; set; }
        public bool EventStatus { get; set; }

        public City City { get; set; }
        public Category Category { get; set; }
        public Organizer Organizer { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
