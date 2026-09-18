using HR_MVC_ITI.Models.Enumes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.Enitityes
{
    public class ApplicationProcess
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }

        public int RecruitmentId { get; set; }
        public Recruitment? Recruitment { get; set; }

        [Required]
        public ApplicationStage CurrentStage { get; set; }

        [Required]
        public DateTime AppliedDate { get; set; }

        public ICollection<ApplicationInterview> Interviews { get; set; } = new List<ApplicationInterview>();
        public ICollection<ApplicationOffer> Offers { get; set; } = new List<ApplicationOffer>();
    }
}
