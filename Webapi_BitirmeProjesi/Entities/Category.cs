using System.Collections.Generic;

namespace Webapi_BitirmeProjesi.Entities
{
    public class Category
    {
        public Category()
        {
            Events= new HashSet<Event>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
