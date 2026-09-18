namespace HR_MVC_ITI.Models.Enitityes;

public class WorkSchedule
{
    public int Id { get; set; }

    public TimeSpan CheckInTime { get; set; }

    public TimeSpan CheckOutTime { get; set; }

    public bool IsActive { get; set; } = true;
}
