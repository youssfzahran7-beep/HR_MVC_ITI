using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers;

public class RecruitmentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RecruitmentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var recruitments = await _unitOfWork.Recruitments.GetAllAsync();
        var recruitmentViewModels = _mapper.Map<IEnumerable<RecruitmentViewModel>>(recruitments);
        return View(recruitmentViewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment == null)
        {
            return NotFound();
        }

        var recruitmentViewModel = _mapper.Map<RecruitmentViewModel>(recruitment);
        return View(recruitmentViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecruitmentViewModel recruitmentViewModel)
    {
        if (ModelState.IsValid)
        {
            var recruitment = _mapper.Map<Recruitment>(recruitmentViewModel);
            await _unitOfWork.Recruitments.AddAsync(recruitment);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(recruitmentViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment == null)
        {
            return NotFound();
        }

        var recruitmentViewModel = _mapper.Map<RecruitmentViewModel>(recruitment);
        return View(recruitmentViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RecruitmentViewModel recruitmentViewModel)
    {
        if (id != recruitmentViewModel.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var recruitment = _mapper.Map<Recruitment>(recruitmentViewModel);
            await _unitOfWork.Recruitments.UpdateAsync(recruitment);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(recruitmentViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment == null)
        {
            return NotFound();
        }

        var recruitmentViewModel = _mapper.Map<RecruitmentViewModel>(recruitment);
        return View(recruitmentViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment != null)
        {
            _unitOfWork.Recruitments.Delete(recruitment);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
