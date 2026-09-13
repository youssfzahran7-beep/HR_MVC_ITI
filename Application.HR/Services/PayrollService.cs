using Application.HR.DTOs;
using Application.HR.IServices;
using AutoMapper;
using Domain.HR.IRepository;
using HRSystem.Domain.Entities;

namespace Application.HR.Services;

public class PayrollService : IPayrollService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PayrollService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PayrollDTO>> GetAllAsync()
    {
        var payrolls = await _unitOfWork.Payrolls.GetAllAsync();
        return _mapper.Map<IEnumerable<PayrollDTO>>(payrolls);
    }

    public async Task<PayrollDTO?> GetByIdAsync(int id)
    {
        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);
        return payroll == null ? null : _mapper.Map<PayrollDTO>(payroll);
    }

    public async Task AddAsync(PayrollDTO dto)
    {
        var payroll = _mapper.Map<Payroll>(dto);
        await _unitOfWork.Payrolls.AddAsync(payroll);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(PayrollDTO dto)
    {
        var payroll = _mapper.Map<Payroll>(dto);
        await _unitOfWork.Payrolls.UpdateAsync(payroll);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);
        if (payroll != null)
        {
            _unitOfWork.Payrolls.Delete(payroll);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
