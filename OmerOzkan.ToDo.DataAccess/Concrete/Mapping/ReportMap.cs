using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.DataAccess.Concrete.Mapping
{
    class ReportMap : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(I => I.Id);
            //builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.Description).HasMaxLength(100).IsRequired();
            //builder.Property(I => I.Detail).HasColumnType("ntext");

            builder.HasOne(I => I.Duty).WithMany(I => I.Reports).HasForeignKey(I => I.DutyId);
        }
    }
}
