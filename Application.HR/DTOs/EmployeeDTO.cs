namespace Application.HR.DTOs;

public class EmployeeDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? ProfileImagePath { get; set; }
}
