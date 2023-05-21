using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corp.ERP.Inventory.Persistence.Configurations;

internal class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("INV_EQP_Equipment");

        builder.Property(e => e.Id)
            .HasColumnName("EQP_Id")
            .ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).HasColumnName("EQP_Name")
            .IsRequired()
            .HasMaxLength(256);
        builder.Property(e => e.Description).HasColumnName("EQP_Description")
            .HasMaxLength(2000);
        
        builder.Property(e => e.Code).HasColumnName("EQP_Code")
            .IsRequired().HasMaxLength(256);
        builder.HasIndex(e => e.Code)
            .IsUnique();

        builder.Property(e => e.IsInUse).HasColumnName("EQP_IsInUse");
        builder.Property(e => e.StartDateUsage).HasColumnName("EQP_StartDateUsage")
            .IsRequired(false);

        builder.Property(e => e.UsedById).HasColumnName("EQP_UsedById")
            .IsRequired(false);
        builder.HasOne(o => o.UsedBy).WithMany().HasForeignKey(k => k.UsedById)
            .IsRequired(false);

        builder.Property(e => e.StorageUnitId).HasColumnName("EQP_StorageUnitId")
            .IsRequired(false);
        builder.HasOne(o => o.StorageUnit).WithMany().HasForeignKey(k => k.StorageUnitId)
            .IsRequired(false);




    }
}
