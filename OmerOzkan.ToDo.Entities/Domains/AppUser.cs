using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OmerOzkan.ToDo.Entities.Domains
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Picture { get; set; }
        public List<Duty> Duties { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
