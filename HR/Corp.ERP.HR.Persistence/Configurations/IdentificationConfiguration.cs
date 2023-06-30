using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.HR.Domain.Models;

namespace Corp.ERP.HR.Persistence.Configurations;

internal class IdentificationConfiguration : IEntityTypeConfiguration<Identification>
{
    public void Configure(EntityTypeBuilder<Identification> builder)
    {
        builder.ToTable("HR_IDE_Identification");

        builder.Property(e => e.Id)
            .HasColumnName("IDE_Id")
            .ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UniqueIdentifier).HasColumnName("IDE_UniqueIdentifier")
                .IsRequired().HasMaxLength(256);

        builder.Property(e => e.StartDate).HasColumnName("IDE_StartDate")
            .IsRequired(true);
        builder.Property(e => e.ExpiryDate).HasColumnName("IDE_ExpiryDate")
            .IsRequired(false);

        builder.Property(e => e.OwnerId).HasColumnName("IDE_OwnerId")
            .IsRequired(true);
        builder.HasOne(o => o.Owner).WithMany().HasForeignKey(k => k.OwnerId)
            .IsRequired(true);

        builder.Property(e => e.IdentificationTypeId).HasColumnName("IDE_IdentificationTypeId")
            .IsRequired(true);
        builder.HasOne(o => o.IdentificationType).WithMany().HasForeignKey(k => k.IdentificationTypeId)
            .IsRequired(true);

        // identifier unique index
        builder.HasIndex(e => new { e.UniqueIdentifier, e.IdentificationTypeId })
            .IsUnique();
    }
}
