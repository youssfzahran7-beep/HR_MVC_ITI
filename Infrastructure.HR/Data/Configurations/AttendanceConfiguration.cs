using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.HR.Data.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Date)
            .IsRequired();

        builder.Property(a => a.OvertimeHours)
            .HasColumnType("decimal(5,2)");

        builder.HasOne(a => a.Employee)
            .WithMany(e => e.Attendances)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Attendances");
    }
}
