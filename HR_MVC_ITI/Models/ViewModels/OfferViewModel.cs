using HR_MVC_ITI.Models.Enumes;
using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.ViewModels
    {
        public class OfferViewModel
        {
            public int Id { get; set; }

            [Required(ErrorMessage ="Id for operator system")]
            [Display(Name = "ID Offer ")]
            public int ApplicationProcessId { get; set; }

            [Required(ErrorMessage = " Besic Salary")]
            [Range(0, double.MaxValue, ErrorMessage ="Salary must be positive number ,please")]
            [Display(Name = "Besic Salary ")]
            public decimal BasicSalaryOffer { get; set; }

            [Display(Name = " Status")]
            public OfferStatus Status { get; set; } = OfferStatus.Pending;
        }
    }

