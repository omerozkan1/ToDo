using System.Collections.Generic;

namespace OmerOzkan.ToDo.Entities.Domains
{
    public class Duty : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UrgencyId { get; set; }
        public Urgency Urgency { get; set; }
        public string AppUserId { get; set; }
        public List<Report> Reports { get; set; }
    }
}
