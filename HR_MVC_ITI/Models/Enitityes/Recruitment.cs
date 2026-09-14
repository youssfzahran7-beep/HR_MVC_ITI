using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using HR_MVC_ITI.Models.Enumes;

namespace HR_MVC_ITI.Models.Enitityes;

public class Recruitment
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)] 
    public string Title { get; set; } = string.Empty;

    [Required] 
    public string Description { get; set; } = string.Empty;

    [Required] 
    public string Requirements { get; set; } = string.Empty;

    public RecruitmentStatus Status { get; set; } = RecruitmentStatus.Open;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ApplicationProcess> Applications { get; set; } = new List<ApplicationProcess>();
}
