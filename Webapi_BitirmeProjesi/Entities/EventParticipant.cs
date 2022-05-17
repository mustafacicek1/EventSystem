namespace Webapi_BitirmeProjesi.Entities
{
    public class EventParticipant
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public bool ParticipationStatus { get; set; }

        public Event Event { get; set; }
        public Participant Participant { get; set; }
    }
}
