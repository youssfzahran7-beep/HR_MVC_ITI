using HR_MVC_ITI.Models.Enumes;

namespace HR_MVC_ITI.Models.Enitityes;

public class ApplicationOffer
{
    public int Id { get; set; }

    public int ApplicationProcessId { get; set; }

    public virtual ApplicationProcess? ApplicationProcess { get; set; }

    public decimal BasicSalaryOffer { get; set; }

    public OfferStatus Status { get; set; } = OfferStatus.Pending;
}
