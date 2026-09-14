using HR_MVC_ITI.Models.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_MVC_ITI.Data.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.BasicSalary)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.StartDate)
            .IsRequired();

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(c => c.Employee)
            .WithMany(e => e.Contracts)
            .HasForeignKey(c => c.EmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("Contracts");
    }
}
