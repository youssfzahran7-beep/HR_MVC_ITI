using HR_MVC_ITI.Models.Enumes;
namespace HR_MVC_ITI.Models.ViewModels;
public class RecruitmentViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public RecruitmentStatus Status { get; set; } = RecruitmentStatus.Open;
    public DateTime CreatedAt { get; set; }
}
