using OmerOzkan.ToDo.Entities.Domains;
using System;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Dto.Dtos.DutyDtos
{
    public class DutyListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UrgencyId { get; set; }
        public Urgency Urgency { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ReportId { get; set; }
        public List<Report> Reports { get; set; }
    }
}
