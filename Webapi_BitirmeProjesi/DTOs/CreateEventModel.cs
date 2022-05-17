using System;

namespace Webapi_BitirmeProjesi.DTOs
{
    public class CreateEventModel
    {
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
    }
}
