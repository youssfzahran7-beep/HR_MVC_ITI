using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.ViewModels;

public class WorkScheduleViewModel
{
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [Display(Name = "Check In Time")]
    public TimeSpan CheckInTime { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [Display(Name = "Check Out Time")]
    public TimeSpan CheckOutTime { get; set; }

    public bool IsActive { get; set; } = true;
}
