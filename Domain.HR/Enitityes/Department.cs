using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HRSystem.Domain.Entities;

public class Department
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)] 
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
