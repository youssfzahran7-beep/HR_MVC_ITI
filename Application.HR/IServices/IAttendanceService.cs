using Application.HR.DTOs;

namespace Application.HR.IServices;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceDTO>> GetAllAsync();
    Task<AttendanceDTO?> GetByIdAsync(int id);
    Task AddAsync(AttendanceDTO dto);
    Task UpdateAsync(AttendanceDTO dto);
    Task DeleteAsync(int id);
}
