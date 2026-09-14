using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.ViewModels;

public class ContractViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Employee ID")]
    public int EmployeeId { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Basic Salary")]
    public decimal BasicSalary { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
}
