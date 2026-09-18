using System;

namespace HR_MVC_ITI.Models.Enitityes;

public class Attendance
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public DateTime Date { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public int LateMinutes { get; set; }

    public decimal OvertimeHours { get; set; }
}
