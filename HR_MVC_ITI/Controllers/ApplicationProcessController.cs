using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.Enumes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_MVC_ITI.Controllers;

public class ApplicationProcessController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ApplicationProcessController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var applications = await _unitOfWork.ApplicationProcesses.GetAllAsync();
        var applicationViewModels = _mapper.Map<List<ApplicationProcessViewModel>>(applications);
        return View(applicationViewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var application = await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        var applicationViewModel = _mapper.Map<ApplicationProcessViewModel>(application);
        return View(applicationViewModel);
    }

    public async Task<IActionResult> Create()
    {
        await Lists();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApplicationProcessViewModel applicationViewModel)
    {
        if (ModelState.IsValid)
        {
            applicationViewModel.AppliedDate = DateTime.Now;
            applicationViewModel.CurrentStage = ApplicationStage.Applied;

            var applicationProcess = _mapper.Map<ApplicationProcess>(applicationViewModel);
            await _unitOfWork.ApplicationProcesses.AddAsync(applicationProcess);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        await Lists(applicationViewModel.CandidateId, applicationViewModel.RecruitmentId);
        return View(applicationViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ApplicationProcessViewModel applicationViewModel)
    {
        if (id != applicationViewModel.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var applicationProcess = await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

            if (applicationProcess == null)
            {
                return NotFound();
            }

            applicationProcess.CurrentStage = applicationViewModel.CurrentStage;
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(applicationViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var application = await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        var applicationViewModel = _mapper.Map<ApplicationProcessViewModel>(application);
        return View(applicationViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var application = await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

        if (application != null)
        {
            _unitOfWork.ApplicationProcesses.Delete(application);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task Lists(int? candidate = null, int? requirement = null)
    {
        var candidates = await _unitOfWork.Candidates.GetAllAsync();
        var recruitments = await _unitOfWork.Recruitments.GetAllAsync();

        ViewBag.Candidates = new SelectList(
            candidates.Select(x => new { x.Id, Name = x.FirstName + " " + x.LastName }),
            "Id",
            "Name",
            candidate);

        ViewBag.Requirements = new SelectList(recruitments, "Id", "Title", requirement);
    }
}
