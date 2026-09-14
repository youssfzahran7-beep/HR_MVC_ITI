using HR_MVC_ITI.Models.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_MVC_ITI.Data.Configurations;

public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
{
    public void Configure(EntityTypeBuilder<Payroll> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Month)
            .IsRequired();

        builder.Property(p => p.Year)
            .IsRequired();

        builder.Property(p => p.BasicSalary)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.LateDeductions)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.OvertimeAdditions)
            .HasColumnType("decimal(18,2)");


        builder.HasOne(p => p.Employee)
            .WithMany(e => e.Payrolls)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Payrolls");
    }
}
