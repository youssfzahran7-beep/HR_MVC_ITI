using Application.HR.DTOs;

namespace Application.HR.IServices;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDTO>> GetAllAsync();
    Task<DepartmentDTO?> GetByIdAsync(int id);
    Task AddAsync(DepartmentDTO dto);
    Task UpdateAsync(DepartmentDTO dto);
    Task DeleteAsync(int id);
}
