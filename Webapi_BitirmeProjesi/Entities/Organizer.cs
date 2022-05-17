using System.Collections.Generic;

namespace Webapi_BitirmeProjesi.Entities
{
    public class Organizer
    {
        public Organizer()
        {
            Events= new HashSet<Event>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
