using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.HR.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(e => e.NationalId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Department)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.ProfileImagePath)
            .HasMaxLength(500);

        builder.HasMany(e => e.Contracts)
            .WithOne(c => c.Employee)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Attendances)
            .WithOne(a => a.Employee)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Payrolls)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Employees");
    }
}
