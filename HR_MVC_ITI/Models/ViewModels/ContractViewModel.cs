using System;
using System.ComponentModel.DataAnnotations;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.Enumes;

namespace HR_MVC_ITI.Models.ViewModels;

public class ContractViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Employee is required")]
    [Display(Name = "Employee")]
    public int EmployeeId { get; set; }

    // Direct Navigation Property
    public Employee? Employee { get; set; }

    [Display(Name = "Employee Name")]
    public string? EmployeeName { get; set; }

    [Required(ErrorMessage = "Basic Salary is required")]
    [DataType(DataType.Currency)]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
    [Display(Name = "Basic Salary")]
    public decimal BasicSalary { get; set; }

    [Required(ErrorMessage = "Start Date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Required]
    [Display(Name = "Contract Status")]
    public ContractStatus Status { get; set; } = ContractStatus.Draft;
}
