using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.ViewModels;

public class PayrollViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Employee ID")]
    public int EmployeeId { get; set; }

    [Required]
    [Range(1, 12)]
    public int Month { get; set; }

    [Required]
    [Range(2000, 2100)]
    public int Year { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Basic Salary")]
    public decimal BasicSalary { get; set; }

    [Display(Name = "Late Deductions")]
    public decimal LateDeductions { get; set; }

    [Display(Name = "Overtime Additions")]
    public decimal OvertimeAdditions { get; set; }

    [Display(Name = "Net Salary")]
    public decimal NetSalary { get; set; }
}
