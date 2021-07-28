using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Concrete.Mapping
{
    public class UrgencyMap : IEntityTypeConfiguration<Urgency>
    {
        public void Configure(EntityTypeBuilder<Urgency> builder)
        {
            builder.Property(I => I.Description).HasMaxLength(100);
        }
    }
}
