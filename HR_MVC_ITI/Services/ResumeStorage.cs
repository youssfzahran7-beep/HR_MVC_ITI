using Microsoft.AspNetCore.Http;

namespace HR_MVC_ITI.Services;

public class ResumeStorage : IResumeStorage
{
    private const string ResumeFolder = "uploads/resumes";
    private readonly IFileSystem _fileSystem;
    private readonly IWebHostEnvironment _environment;

    public ResumeStorage(IFileSystem fileSystem, IWebHostEnvironment environment)
    {
        _fileSystem = fileSystem;
        _environment = environment;
    }

    public async Task<string> SaveAsync(IFormFile resume, CancellationToken cancellationToken = default)
    {
        var extension = _fileSystem.GetExtension(resume.FileName).ToLowerInvariant();
        var directory = _fileSystem.Combine(_environment.WebRootPath, "uploads", "resumes");
        _fileSystem.CreateDirectory(directory);

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var physicalPath = _fileSystem.Combine(directory, fileName);
        await using var output = _fileSystem.CreateFile(physicalPath);
        await resume.CopyToAsync(output, cancellationToken);

        return $"/{ResumeFolder}/{fileName}";
    }
}
