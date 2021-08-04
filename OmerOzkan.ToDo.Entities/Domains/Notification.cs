namespace OmerOzkan.ToDo.Entities.Domains
{
    public class Notification : BaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
    }
}
