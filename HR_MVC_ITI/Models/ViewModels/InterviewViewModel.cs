using System;
using System.ComponentModel.DataAnnotations;
using HR_MVC_ITI.Models.Enitityes;

namespace HR_MVC_ITI.ViewModels
{
    public class InterviewViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Application Process is Required")]
        [Display(Name = "Application Process")]
        public int ApplicationProcessId { get; set; }

        // Navigation property to ApplicationProcess
        public ApplicationProcess? ApplicationProcess { get; set; }

        // Compatibility alias as requested (Idapplicationprocess)
        public int Idapplicationprocess
        {
            get => ApplicationProcessId;
            set => ApplicationProcessId = value;
        }

        [Display(Name = "Interviewer ID")]
        public string? InterviewerID { get; set; }

        [Required(ErrorMessage = "Interview Date is Required")]
        [Display(Name = "Interview Date")]
        public DateTime InterviewDate { get; set; } = DateTime.Now;

        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public decimal? Score { get; set; }

        [StringLength(500, ErrorMessage = "FeedBack Cannot exceed 500 Characters")]
        public string? FeedBack { get; set; }

        [Display(Name = "Candidate Name / Application")]
        public string? ApplicantionName { get; set; }
    }
}