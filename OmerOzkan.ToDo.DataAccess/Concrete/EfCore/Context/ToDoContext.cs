using Microsoft.EntityFrameworkCore;
using OmerOzkan.ToDo.DataAccess.Concrete.Mapping;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Concrete.EfCore.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new DutyMap());
            builder.ApplyConfiguration(new NotificationMap());
            builder.ApplyConfiguration(new ReportMap());
            builder.ApplyConfiguration(new UrgencyMap());

            base.OnModelCreating(builder);
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Urgency> Urgencies { get; set; }
    }
}
