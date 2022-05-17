using System.Collections.Generic;

namespace Webapi_BitirmeProjesi.Entities
{
    public class City
    {
        public City()
        {
            Events= new HashSet<Event>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
