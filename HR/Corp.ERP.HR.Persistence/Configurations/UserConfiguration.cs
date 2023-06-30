using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Corp.ERP.HR.Domain.Models;

namespace Corp.ERP.HR.Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("HR_USR_User");

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

        builder.Property(e => e.BirthdayDate).HasColumnName("USR_BirthdayDate")
            .IsRequired(true);
        builder.Property(e => e.RecruitmentDate).HasColumnName("USR_RecruitmentDate")
            .IsRequired(true);
        builder.Property(e => e.LeavingDate).HasColumnName("USR_LeavingDate")
            .IsRequired(false);
        builder.Property(e => e.CurrentSalaryGross).HasColumnName("USR_CurrentSalaryGross")
            .IsRequired(true)
            .HasPrecision(3);
        builder.Property(e => e.CurrentSalaryNet).HasColumnName("USR_CurrentSalaryNet")
            .IsRequired(true)
            .HasPrecision(3);
        builder.Property(e => e.IsDeleted).HasColumnName("USR_IsDeleted")
            .IsRequired(false);

        builder.Property(e => e.CurrentTitleId).HasColumnName("EQP_CurrentTitleId")
            .IsRequired(true);
        builder.HasOne(o => o.CurrentTitle).WithMany().HasForeignKey(k => k.CurrentTitleId)
            .IsRequired(true);

        builder.Property(e => e.ManagedById).HasColumnName("EQP_ManagedById")
            .IsRequired(false);
        builder.HasOne(o => o.ManagedBy).WithMany().HasForeignKey(k => k.ManagedById)
            .IsRequired(false);

        builder.HasMany(e => e.Identifications).WithOne(e => e.Owner)
            .HasForeignKey(e => e.OwnerId).IsRequired(true);

    }
}
