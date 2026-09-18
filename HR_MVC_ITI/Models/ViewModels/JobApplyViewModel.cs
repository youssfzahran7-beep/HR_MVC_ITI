using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace HR_MVC_ITI.Models.ViewModels;

public class JobApplyViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Select a requirement.")]
    public int RecruitmentId { get; set; }

    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    public IFormFile? Resume { get; set; }
}
