using Microsoft.AspNetCore.Http;

namespace HR_MVC_ITI.Services;

public interface IResumeStorage
{
    Task<string> SaveAsync(IFormFile resume, CancellationToken cancellationToken = default);
}
