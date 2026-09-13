using Application.HR.DTOs;
using Application.HR.IServices;
using AutoMapper;
using Domain.HR.IRepository;
using HRSystem.Domain.Entities;

namespace Application.HR.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
    }

    public async Task<EmployeeDTO?> GetByIdAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        return employee == null ? null : _mapper.Map<EmployeeDTO>(employee);
    }

    public async Task AddAsync(EmployeeDTO dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(EmployeeDTO dto)
    {
        var employee = _mapper.Map<Employee>(dto);
        await _unitOfWork.Employees.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee != null)
        {
            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
