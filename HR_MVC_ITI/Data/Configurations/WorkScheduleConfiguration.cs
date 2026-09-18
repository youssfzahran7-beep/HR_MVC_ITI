using HR_MVC_ITI.Models.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_MVC_ITI.Data.Configurations;

public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
{
    public void Configure(EntityTypeBuilder<WorkSchedule> builder)
    {
        builder.HasKey(schedule => schedule.Id);

        builder.Property(schedule => schedule.CheckInTime)
            .IsRequired();

        builder.Property(schedule => schedule.CheckOutTime)
            .IsRequired();

        builder.ToTable("WorkSchedules");
    }
}
