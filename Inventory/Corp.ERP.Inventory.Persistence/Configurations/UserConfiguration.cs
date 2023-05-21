using Corp.ERP.Inventory.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corp.ERP.Inventory.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("INV_USR_User");

            builder.Property(e => e.Id)
                .HasColumnName("USR_Id")
                .ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).HasColumnName("USR_FirstName")
                .IsRequired(false)
                .HasMaxLength(256);
            builder.Property(e => e.LastName).HasColumnName("USR_LastName")
                .IsRequired(false)
                .HasMaxLength(256);
            builder.Property(e => e.Email).HasColumnName("USR_Email")
                .IsRequired(true)
                .HasMaxLength(256);
            
        }
    }
}
