using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TalentManagementApi.Domain.Entities;

namespace TalentManagementApi.Infrastructure.Persistence.Contexts
{
    public partial class ApplicationDbContext
    {
        internal class PositionConfiguration : IEntityTypeConfiguration<Position>
        {
            public void Configure(EntityTypeBuilder<Position> builder)
            {
                builder.ToTable("Positions");
                builder.Property(e => e.Id).ValueGeneratedNever();
                builder.Property(e => e.PositionDescription)
                    .IsRequired()
                    .HasMaxLength(1000);
                builder.Property(e => e.PositionNumber)
                    .IsRequired()
                    .HasMaxLength(100);
                builder.Property(e => e.PositionTitle)
                    .IsRequired()
                    .HasMaxLength(250);
                builder.Property(e => e.CreatedBy).HasMaxLength(100);
                builder.Property(e => e.LastModifiedBy).HasMaxLength(100);
            }
        }
    }
}