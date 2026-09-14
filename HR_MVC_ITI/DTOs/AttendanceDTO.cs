namespace HR_MVC_ITI.DTOs;

public class AttendanceDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public int LateMinutes { get; set; }
    public decimal OvertimeHours { get; set; }
}
