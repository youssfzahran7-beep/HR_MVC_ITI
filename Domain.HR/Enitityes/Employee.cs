using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRSystem.Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    [Required]
    [StringLength(50)] 
    public string NationalId { get; set; } = string.Empty;

    [Required]
    [StringLength(100)] 
    public string Department { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? ProfileImagePath { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}
