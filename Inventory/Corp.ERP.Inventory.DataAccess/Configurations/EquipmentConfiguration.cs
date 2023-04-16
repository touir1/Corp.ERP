using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corp.ERP.Inventory.Persistence.Configurations;

internal class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("INV_EQP_Equipment");

        builder.HasKey(e => e.Id);

        builder
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("EQP_Name");
        builder
            .Property(e => e.Description)
            .HasMaxLength(2000)
            .HasColumnName("EQP_Description");
        builder
            .Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName("EQP_Code");
        builder.Property(e => e.IsInUse).HasColumnName("EQP_IsInUse");
        builder.Property(e => e.StartDateUsage).HasColumnName("EQP_StartDateUsage");
        builder.Property(e => e.UsedById).HasColumnName("EQP_UsedById");

        builder
            .HasOne(o => o.UsedBy)
            .WithMany()
            .HasForeignKey(k => k.UsedById)
            .IsRequired(false);

    }
}
