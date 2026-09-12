using Application.HR.DTOs;
using Application.HR.IServices;
using AutoMapper;
using Domain.HR.IRepository;
using HRSystem.Domain.Entities;

namespace Application.HR.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
    {
        var departments = await _unitOfWork.Departments.GetAllAsync();
        return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
    }

    public async Task<DepartmentDTO?> GetByIdAsync(int id)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(id);
        return department == null ? null : _mapper.Map<DepartmentDTO>(department);
    }

    public async Task AddAsync(DepartmentDTO dto)
    {
        var department = _mapper.Map<Department>(dto);
        await _unitOfWork.Departments.AddAsync(department);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(DepartmentDTO dto)
    {
        var department = _mapper.Map<Department>(dto);
        await _unitOfWork.Departments.UpdateAsync(department);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _unitOfWork.Departments.GetByIdAsync(id);
        if (department != null)
        {
            _unitOfWork.Departments.Delete(department);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
