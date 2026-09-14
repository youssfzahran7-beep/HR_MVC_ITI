using HR_MVC_ITI.Models.Enitityes;

namespace HR_MVC_ITI.Models.IRepository;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Employee> Employees { get; }
    IGenericRepository<Attendance> Attendances { get; }
    IGenericRepository<Contract> Contracts { get; }
    IGenericRepository<Payroll> Payrolls { get; }
    IGenericRepository<Candidate> Candidates { get; }
    IGenericRepository<ApplicationProcess> ApplicationProcesses { get; }

    Task<int> SaveChangesAsync();
}
