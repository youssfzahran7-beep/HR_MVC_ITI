using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.Enumes;
using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.ViewModels
{
    public class OfferViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Application Process ID is required")]
        [Display(Name = "Application Process")]
        public int ApplicationProcessId { get; set; }

        public ApplicationProcess? ApplicationProcess { get; set; }

        [Display(Name = "Related Interview")]
        public int? InterviewId { get; set; }

        public ApplicationInterview? ApplicationInterview { get; set; }

        [Required(ErrorMessage = "Basic Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        [Display(Name = "Basic Salary")]
        public decimal BasicSalaryOffer { get; set; }

        [Display(Name = "Status")]
        public OfferStatus Status { get; set; } = OfferStatus.Pending;

        [Display(Name = "Candidate Name")]
        public string? CandidateName { get; set; }
    }
}
