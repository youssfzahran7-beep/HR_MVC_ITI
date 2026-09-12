using Application.HR.DTOs;

namespace Application.HR.IServices;

public interface IContractService
{
    Task<IEnumerable<ContractDTO>> GetAllAsync();
    Task<ContractDTO?> GetByIdAsync(int id);
    Task AddAsync(ContractDTO dto);
    Task UpdateAsync(ContractDTO dto);
    Task DeleteAsync(int id);
}
