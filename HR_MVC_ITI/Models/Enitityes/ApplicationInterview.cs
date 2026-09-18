using System;
using System.Collections.Generic;
using HR_MVC_ITI.Models.Enumes;

namespace HR_MVC_ITI.Models.Enitityes;

public class ApplicationInterview
{
    public int Id { get; set; }

    public int ApplicationProcessId { get; set; }

    public ApplicationProcess? ApplicationProcess { get; set; }

    public string Interviewerid{ get; set; } = string.Empty;

    public DateTime ScheduledDate { get; set; }

    public decimal? Score { get; set; }

    public string? Feedback { get; set; }

    public ICollection<ApplicationOffer> Offers { get; set; } = new List<ApplicationOffer>();
}
