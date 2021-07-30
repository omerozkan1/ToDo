using OmerOzkan.ToDo.Entities.Domains;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Dto.Dtos.UrgencyDtos
{
    public class UrgencyListDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Duty> Duties { get; set; }
    }
}
