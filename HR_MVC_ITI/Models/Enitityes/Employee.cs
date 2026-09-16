using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HR_MVC_ITI.Models.Enitityes;

public class Employee
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser? User { get; set; }

    public string NationalId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
}
