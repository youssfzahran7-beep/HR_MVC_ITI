namespace HR_MVC_ITI.DTOs;

public class ContractDTO
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal BasicSalary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
