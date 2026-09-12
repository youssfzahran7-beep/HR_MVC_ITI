using Application.HR.DTOs;

namespace Application.HR.IServices;

public interface IPayrollService
{
    Task<IEnumerable<PayrollDTO>> GetAllAsync();
    Task<PayrollDTO?> GetByIdAsync(int id);
    Task AddAsync(PayrollDTO dto);
    Task UpdateAsync(PayrollDTO dto);
    Task DeleteAsync(int id);
}
