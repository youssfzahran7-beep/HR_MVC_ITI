using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.ViewModels;

public class EmployeeViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(200, ErrorMessage = "Full Name cannot exceed 200 characters.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "National ID is required.")]
    [StringLength(50, ErrorMessage = "National ID cannot exceed 50 characters.")]
    [Display(Name = "National ID")]
    public string NationalId { get; set; } = string.Empty;

    [Required(ErrorMessage = "User Account is required.")]
    [Display(Name = "Select User Account")]
    public string UserId { get; set; } = string.Empty;
}
