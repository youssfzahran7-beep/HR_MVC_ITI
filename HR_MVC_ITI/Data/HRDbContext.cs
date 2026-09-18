using HR_MVC_ITI.Models.Enitityes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_MVC_ITI.Data;

public class HRDbContext : IdentityDbContext<ApplicationUser>
{
    public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Attendance> Attendances { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<Payroll> Payrolls { get; set; } = null!;
    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<ApplicationProcess> ApplicationProcesses { get; set; } = null!;
    public DbSet<Recruitment> Recruitments { get; set; } = null!;
    public DbSet<ApplicationInterview> ApplicationInterviews { get; set; } = null!;
    public DbSet<ApplicationOffer> ApplicationOffers { get; set; } = null!;
    public DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDbContext).Assembly);
    }
}
