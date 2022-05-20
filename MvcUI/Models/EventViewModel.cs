using System;

namespace MvcUI.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime LastApplicationDate { get; set; }
        public string OrganizerName { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int MaxParticipant { get; set; }
        public int ParticipantCount { get; set; }
        public decimal? TicketPrice { get; set; }
    }
}
