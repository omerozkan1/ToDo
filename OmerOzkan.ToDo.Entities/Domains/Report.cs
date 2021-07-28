namespace OmerOzkan.ToDo.Entities.Domains
{
    public class Report : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public int DutyId { get; set; }
        public Duty Duty { get; set; }
    }
}
