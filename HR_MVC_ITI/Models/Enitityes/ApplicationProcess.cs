using HR_MVC_ITI.Models.Enumes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HR_MVC_ITI.Models.Enitityes
{
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
