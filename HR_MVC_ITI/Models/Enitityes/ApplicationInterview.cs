using System;
using HR_MVC_ITI.Models.Enumes;
using HR_MVC_ITI.ViewModels;

namespace HR_MVC_ITI.Models.Enitityes;
public class ApplicationInterview
{
    public int Id { get; set; }

    public int ApplicationProcessId { get; set; }

    public ApplicationProcess? ApplicationProcess { get; set; }

    public string InterviewerId { get; set; } = string.Empty;

    public DateTime ScheduledDate { get; set; }

    public decimal? Score { get; set; }

    public string? Feedback { get; set; }
}
