namespace HR_MVC_ITI.DTOs;

public class PayrollDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal BasicSalary { get; set; }
    public decimal LateDeductions { get; set; }
    public decimal OvertimeAdditions { get; set; }
    public decimal NetSalary { get; set; }
}
