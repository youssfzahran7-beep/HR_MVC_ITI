using HR_MVC_ITI.Models.Enumes;

namespace HR_MVC_ITI.DTOs
{
    public class ApplicationProcessDTO
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int RecruitmentId { get; set; }

        public ApplicationStage CurrentStage { get; set; }

        public DateTime AppliedDate { get; set; }
    }
}
