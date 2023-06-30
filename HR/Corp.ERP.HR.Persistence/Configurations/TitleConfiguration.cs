using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.HR.Domain.Models;

namespace Corp.ERP.HR.Persistence.Configurations
{
    internal class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.ToTable("HR_TTL_Title");

            builder.Property(e => e.Id)
                .HasColumnName("TTL_Id")
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).HasColumnName("TTL_Code")
                .IsRequired().HasMaxLength(256);
            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Label).HasColumnName("TTL_Label")
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(e => e.Description).HasColumnName("TTL_Description")
                .HasMaxLength(2000);
        }
    }
}
