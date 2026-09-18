using HR_MVC_ITI.Models.Enumes;
namespace HR_MVC_ITI.Models.ViewModels;
public class ApplicationProcessViewModel
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    public int RecruitmentId { get; set; }
    public string CandidateName { get; set; } = string.Empty;
    public string RequirementTitle { get; set; } = string.Empty;
    public ApplicationStage CurrentStage { get; set; }
    public DateTime AppliedDate { get; set; }
}
