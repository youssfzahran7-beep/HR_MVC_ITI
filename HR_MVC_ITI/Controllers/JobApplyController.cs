using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.Enumes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using HR_MVC_ITI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_MVC_ITI.Controllers;

[AllowAnonymous]
public class JobApplyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IResumeStorage _resumeStorage;

    public JobApplyController(IUnitOfWork unitOfWork, IResumeStorage resumeStorage)
    {
        _unitOfWork = unitOfWork;
        _resumeStorage = resumeStorage;
    }

    public async Task<IActionResult> Index()
    {
        await LoadRequirements();
        return View(new JobApplyViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(JobApplyViewModel model)
    {
        var permitted = new[] { ".pdf", ".doc", ".docx" };
        var extension = model.Resume is null
            ? string.Empty
            : Path.GetExtension(model.Resume.FileName).ToLowerInvariant();

        if (model.Resume is not null && (!permitted.Contains(extension) || model.Resume.Length > 5 * 1024 * 1024))
        {
            ModelState.AddModelError(nameof(model.Resume), "Upload a PDF, DOC, or DOCX up to 5 MB.");
        }

        if (!ModelState.IsValid)
        {
            await LoadRequirements(model.RecruitmentId);
            return View(model);
        }

        var resumePath = await _resumeStorage.SaveAsync(model.Resume!, HttpContext.RequestAborted);

        var candidate = new Candidate
        {
            UserId = $"Applicant-{Guid.NewGuid():N}",
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = model.Phone,
            ResumeFilePath = resumePath
        };

        await _unitOfWork.Candidates.AddAsync(candidate);
        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.ApplicationProcesses.AddAsync(new ApplicationProcess
        {
            CandidateId = candidate.Id,
            RecruitmentId = model.RecruitmentId,
            AppliedDate = DateTime.Now,
            CurrentStage = ApplicationStage.Applied
        });

        await _unitOfWork.SaveChangesAsync();

        return View("Success");
    }

    private async Task LoadRequirements(int? selected = null)
    {
        var requirements = await _unitOfWork.Recruitments.GetAllAsync();
        ViewBag.Requirements = new SelectList(requirements, "Id", "Title", selected);
    }
}
