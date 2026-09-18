using HR_MVC_ITI.Models.Enitityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_MVC_ITI.Data.Configurations;

public class ApplicationOfferConfiguration : IEntityTypeConfiguration<ApplicationOffer>
{
    public void Configure(EntityTypeBuilder<ApplicationOffer> builder)
    {
        builder.ToTable("ApplicationOffers");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.BasicSalaryOffer)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(o => o.ApplicationProcess)
            .WithMany(a => a.Offers)
            .HasForeignKey(o => o.ApplicationProcessId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.ApplicationInterview)
            .WithMany(i => i.Offers)
            .HasForeignKey(o => o.InterviewId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
