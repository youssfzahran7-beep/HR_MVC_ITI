using Domain.HR.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.HR.Data.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.ResumeFilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.ToTable("Candidates");
    }
}
