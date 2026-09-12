using Domain.HR.Enitityes;
using HRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.HR.Data;

public class HRDbContext : DbContext
{
    public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Attendance> Attendances { get; set; } = null!;
    public DbSet<Contract> Contracts { get; set; } = null!;
    public DbSet<Payroll> Payrolls { get; set; } = null!;
    public DbSet<Candidate> Candidates { get; set; } = null!;
    public DbSet<ApplicationProcess> ApplicationProcesses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRDbContext).Assembly);
    }
}
