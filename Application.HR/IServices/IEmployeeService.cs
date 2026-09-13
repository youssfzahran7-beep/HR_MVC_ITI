using Application.HR.DTOs;

namespace Application.HR.IServices;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    Task<EmployeeDTO?> GetByIdAsync(int id);
    Task AddAsync(EmployeeDTO dto);
    Task UpdateAsync(EmployeeDTO dto);
    Task DeleteAsync(int id);
}
