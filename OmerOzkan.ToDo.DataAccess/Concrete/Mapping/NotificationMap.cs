using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Concrete.Mapping
{
    public class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(x=> x.Description).HasMaxLength(100);
        }
    }
}
