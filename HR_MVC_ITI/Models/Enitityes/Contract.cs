using HR_MVC_ITI.Models.Enumes;
using System;

namespace HR_MVC_ITI.Models.Enitityes;

public class Contract
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public decimal BasicSalary { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ContractStatus Status { get; set; } = ContractStatus.Draft;
}
