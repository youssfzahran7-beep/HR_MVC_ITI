using Domain.HR.Enitityes;
using Domain.HR.IRepository;
using HRSystem.Domain.Entities;
using Infrastructure.HR.Data;

namespace Infrastructure.HR.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HRDbContext _context;

    public IGenericRepository<Employee> Employees { get; }
    public IGenericRepository<Department> Departments { get; }
    public IGenericRepository<Attendance> Attendances { get; }
    public IGenericRepository<Contract> Contracts { get; }
    public IGenericRepository<Payroll> Payrolls { get; }
    public IGenericRepository<Candidate> Candidates { get; }
    public IGenericRepository<ApplicationProcess> ApplicationProcesses { get; }

    public UnitOfWork(
        HRDbContext context,
        IGenericRepository<Employee> employees,
        IGenericRepository<Department> departments,
        IGenericRepository<Attendance> attendances,
        IGenericRepository<Contract> contracts,
        IGenericRepository<Payroll> payrolls,
        IGenericRepository<Candidate> candidates,
        IGenericRepository<ApplicationProcess> applicationProcesses)
    {
        _context = context;
        Employees = employees;
        Departments = departments;
        Attendances = attendances;
        Contracts = contracts;
        Payrolls = payrolls;
        Candidates = candidates;
        ApplicationProcesses = applicationProcesses;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
