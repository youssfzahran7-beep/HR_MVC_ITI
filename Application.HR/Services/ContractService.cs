using Application.HR.DTOs;
using Application.HR.IServices;
using AutoMapper;
using Domain.HR.IRepository;
using HRSystem.Domain.Entities;

namespace Application.HR.Services;

public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContractService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContractDTO>> GetAllAsync()
    {
        var contracts = await _unitOfWork.Contracts.GetAllAsync();
        return _mapper.Map<IEnumerable<ContractDTO>>(contracts);
    }

    public async Task<ContractDTO?> GetByIdAsync(int id)
    {
        var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
        return contract == null ? null : _mapper.Map<ContractDTO>(contract);
    }

    public async Task AddAsync(ContractDTO dto)
    {
        var contract = _mapper.Map<Contract>(dto);
        await _unitOfWork.Contracts.AddAsync(contract);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(ContractDTO dto)
    {
        var contract = _mapper.Map<Contract>(dto);
        await _unitOfWork.Contracts.UpdateAsync(contract);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
        if (contract != null)
        {
            _unitOfWork.Contracts.Delete(contract);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
