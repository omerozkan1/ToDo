using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.Dto.Dtos.ReportDtos
{
    public class ReportAddDto
    {
        public string Description { get; set; }
        public string Detail { get; set; }
        public Duty Duty { get; set; }
        public int DutyId { get; set; }
    }
}
