using HR_MVC_ITI.Models.Enitityes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HR_MVC_ITI.Data;

public class HRDbContext : IdentityDbContext<ApplicationUser>
{
    public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<Attendance> Attendances { get; set; } = null!;
    public virtual DbSet<Contract> Contracts { get; set; } = null!;
    public virtual DbSet<Payroll> Payrolls { get; set; } = null!;
    public virtual DbSet<Candidate> Candidates { get; set; } = null!;
    public virtual DbSet<ApplicationProcess> ApplicationProcesses { get; set; } = null!;
    public virtual DbSet<Recruitment> Recruitments { get; set; } = null!;
    public virtual DbSet<ApplicationInterview> ApplicationInterviews { get; set; } = null!;
    public virtual DbSet<ApplicationOffer> ApplicationOffers { get; set; } = null!;
    public virtual DbSet<WorkSchedule> WorkSchedules { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDbContext).Assembly);
    }
}
