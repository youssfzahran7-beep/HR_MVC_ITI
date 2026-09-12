using Application.HR.DTOs;
using Application.HR.IServices;
using AutoMapper;
using Domain.HR.IRepository;
using HRSystem.Domain.Entities;

namespace Application.HR.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AttendanceDTO>> GetAllAsync()
    {
        var attendances = await _unitOfWork.Attendances.GetAllAsync();
        return _mapper.Map<IEnumerable<AttendanceDTO>>(attendances);
    }

    public async Task<AttendanceDTO?> GetByIdAsync(int id)
    {
        var attendance = await _unitOfWork.Attendances.GetByIdAsync(id);
        return attendance == null ? null : _mapper.Map<AttendanceDTO>(attendance);
    }

    public async Task AddAsync(AttendanceDTO dto)
    {
        var attendance = _mapper.Map<Attendance>(dto);
        await _unitOfWork.Attendances.AddAsync(attendance);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(AttendanceDTO dto)
    {
        var attendance = _mapper.Map<Attendance>(dto);
        await _unitOfWork.Attendances.UpdateAsync(attendance);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attendance = await _unitOfWork.Attendances.GetByIdAsync(id);
        if (attendance != null)
        {
            _unitOfWork.Attendances.Delete(attendance);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
