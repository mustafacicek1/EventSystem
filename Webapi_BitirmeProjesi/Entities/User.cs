using System.Collections.Generic;

namespace Webapi_BitirmeProjesi.Entities
{
    public class User
    {
        public User()
        {
            Organizers = new HashSet<Organizer>();
            Participants = new HashSet<Participant>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public ICollection<Organizer> Organizers { get; set; }
        public ICollection<Participant> Participants { get; set; }
    }
}
