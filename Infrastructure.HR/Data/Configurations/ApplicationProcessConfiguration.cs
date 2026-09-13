using Domain.HR.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.HR.Data.Configurations;

public class ApplicationProcessConfiguration : IEntityTypeConfiguration<ApplicationProcess>
{
    public void Configure(EntityTypeBuilder<ApplicationProcess> builder)
    {
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

        builder.ToTable("ApplicationProcesses");
    }
}
