using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corp.ERP.Inventory.Persistence.Configurations
{
    public class StorageConfiguration : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.ToTable("INV_SRG_Storage");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnName("SRG_Name")
                .IsRequired(true)
                .HasMaxLength(256);
            builder.Property(e => e.Address).HasColumnName("SRG_Address")
                .IsRequired(false)
                .HasMaxLength(512);
        }
    }
}
