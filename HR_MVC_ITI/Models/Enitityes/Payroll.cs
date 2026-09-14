using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.Enitityes;

public class Payroll
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }


    public int Month { get; set; }

    public int Year { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal LateDeductions { get; set; }

    public decimal OvertimeAdditions { get; set; }

    public decimal NetSalary => BasicSalary - LateDeductions + OvertimeAdditions;
   
    
    // في الـ Entity — خليها auto-property عادية
  //  public decimal NetSalary { get; set; }

    // في الـ Service أو Controller قبل ما تعمل Save:
    //payroll.NetSalary = payroll.BasicSalary - payroll.LateDeductions + payroll.OvertimeAdditions;
}
