using System.Collections.Generic;

namespace OmerOzkan.ToDo.Entities.Domains
{
    public class Urgency : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Duty> Duties { get; set; }
    }
}
