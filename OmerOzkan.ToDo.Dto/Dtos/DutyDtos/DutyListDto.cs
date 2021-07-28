using OmerOzkan.ToDo.Entities.Domains;
using System;

namespace OmerOzkan.ToDo.Dto.Dtos.DutyDtos
{
    public class DutyListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UrgencyId { get; set; }
        public Urgency Urgency { get; set; }
    }
}
