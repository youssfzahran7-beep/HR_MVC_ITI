using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.HR.Enitityes
{
    public enum ApplicationStage
    {
        Applied = 0,
        Interviewing = 1,
        Offered = 2,
        Hired = 3,
        Rejected = 4
    }
    public class ApplicationProcess
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int RecruitmentId { get; set; }

        [Required]
        public ApplicationStage CurrentStage { get; set; }

        [Required]
        public DateTime AppliedDate { get; set; }
    }
}
