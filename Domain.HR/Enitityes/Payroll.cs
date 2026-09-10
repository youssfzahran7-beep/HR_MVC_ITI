using System.ComponentModel.DataAnnotations;

namespace HRSystem.Domain.Entities;

public class Payroll
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    [Range(1, 12)] 
    public int Month { get; set; }

    public int Year { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal LateDeductions { get; set; }

    public decimal OvertimeAdditions { get; set; }

    public decimal NetSalary { get; set; }
}
