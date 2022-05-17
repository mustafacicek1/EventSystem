using System.Collections.Generic;

namespace Webapi_BitirmeProjesi.Entities
{
    public class Participant
    {
        public Participant()
        {
            EventParticipants = new HashSet<EventParticipant>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<EventParticipant> EventParticipants { get; set; }
    }
}
