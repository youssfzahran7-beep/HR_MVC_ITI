using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Data;

namespace HR_MVC_ITI.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly HRDbContext _context;

    public IGenericRepository<Employee> Employees { get; }
    public IGenericRepository<Attendance> Attendances { get; }
    public IGenericRepository<Payroll> Payrolls { get; }
    public IGenericRepository<Candidate> Candidates { get; }
    public IGenericRepository<ApplicationInterview> Interviews { get; set; }
    public IGenericRepository<ApplicationOffer> Offers { get; }
    public IGenericRepository<Contract> Contracts { get; }
    public IGenericRepository<ApplicationProcess> ApplicationProcesses { get; }
    public IGenericRepository<Recruitment> Recruitments { get; }    

    public UnitOfWork(
        HRDbContext context,
        IGenericRepository<Employee> employees,
        IGenericRepository<Attendance> attendances,
        IGenericRepository<Contract> contracts,
        IGenericRepository<Payroll> payrolls,
        IGenericRepository<Candidate> candidates,
        IGenericRepository<ApplicationInterview> Interviews,
        IGenericRepository<ApplicationProcess> applicationProcesses)
    {
        _context = context;
        Employees = employees;
        Attendances = attendances;
        Contracts = contracts;
        Payrolls = payrolls;
        Candidates = candidates;
        ApplicationProcesses = applicationProcesses;
        Interviews = new GenericRepository<ApplicationInterview>(_context);
        Offers = new GenericRepository<ApplicationOffer>(_context);
    }
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<ApplicationInterview> Interviews { get; set; }
        IGenericRepository<Contract> Contracts { get; set; }
        IGenericRepository<ApplicationOffer> Offers { get; set; }
        
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
