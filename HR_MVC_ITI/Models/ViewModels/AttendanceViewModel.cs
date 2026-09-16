using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.ViewModels;

public class AttendanceViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Employee ID")]
    public int EmployeeId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Check In Time")]
    public DateTime? CheckInTime { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Check Out Time")]
    public DateTime? CheckOutTime { get; set; }

    [Display(Name = "Late Minutes")]
    public int LateMinutes { get; set; }

    [Display(Name = "Overtime Hours")]
    public decimal OvertimeHours { get; set; }
}
