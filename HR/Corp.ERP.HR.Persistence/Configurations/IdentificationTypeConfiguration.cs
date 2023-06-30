using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.HR.Domain.Models;

namespace Corp.ERP.HR.Persistence.Configurations;

internal class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType>
{
    public void Configure(EntityTypeBuilder<IdentificationType> builder)
    {
        builder.ToTable("HR_IDT_IdentificationType");

        builder.Property(e => e.Id)
            .HasColumnName("IDT_Id")
            .ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Code).HasColumnName("IDT_Code")
            .IsRequired().HasMaxLength(256);
        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.Property(e => e.Label).HasColumnName("IDT_Label")
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(e => e.Description).HasColumnName("IDT_Description")
            .HasMaxLength(2000);
    }
}
