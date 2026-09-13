using Domain.HR.Enitityes;
using HRSystem.Domain.Entities;

namespace Domain.HR.IRepository;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Employee> Employees { get; }
    IGenericRepository<Department> Departments { get; }
    IGenericRepository<Attendance> Attendances { get; }
    IGenericRepository<Contract> Contracts { get; }
    IGenericRepository<Payroll> Payrolls { get; }
    IGenericRepository<Candidate> Candidates { get; }
    IGenericRepository<ApplicationProcess> ApplicationProcesses { get; }

    Task<int> SaveChangesAsync();
}
