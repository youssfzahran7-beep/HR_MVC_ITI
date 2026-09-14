using HR_MVC_ITI.Models.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_MVC_ITI.Data.Configurations;

public class ApplicationProcessConfiguration : IEntityTypeConfiguration<ApplicationProcess>
{
    public void Configure(EntityTypeBuilder<ApplicationProcess> builder)
    {
        builder.ToTable("ApplicationProcesses"); 

        builder.HasKey(a => a.Id);

        builder.Property(a => a.CandidateId)
            .IsRequired();

        builder.Property(a => a.RecruitmentId)
            .IsRequired();

        builder.Property(a => a.CurrentStage)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.AppliedDate)
            .IsRequired();


    }
}
