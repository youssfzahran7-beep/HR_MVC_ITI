using System;
using System.ComponentModel.DataAnnotations;
namespace HR_MVC_ITI.ViewModels
{
    public class InterviewViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ApplicationProcess ID is Required")]
        [Display(Name = "ApplicationProcss")]
        public int ApplicationProcess { get; set; }
        [Required(ErrorMessage = "InterviewID is Required")]
        [Display(Name = "InterviewId")]
        public string InterviewerID { get; set; }
        [Required(ErrorMessage = "InterviewDate is Required")]
        [Display(Name = "InterviewDate ")]
        public DateTime InterviewDate { get; set; }
        [Range(0, 100, ErrorMessage = "score must be between 0 and 100")]
        public decimal Score { get; set; }
        [StringLength(500, ErrorMessage = "FeedBack Cannot exceed 500 Characters")]
        public string FeedBack { get; set; }
        public string ApplicantionName { get; set; }
    }
}