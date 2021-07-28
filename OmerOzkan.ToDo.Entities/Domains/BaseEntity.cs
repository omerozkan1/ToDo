using System;

namespace OmerOzkan.ToDo.Entities.Domains
{
    public class BaseEntity
    {
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
