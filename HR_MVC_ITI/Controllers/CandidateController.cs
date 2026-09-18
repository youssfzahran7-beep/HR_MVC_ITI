using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers;

public class CandidateController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CandidateController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var candidates = await _unitOfWork.Candidates.GetAllAsync();
        var candidateViewModels = _mapper.Map<IEnumerable<CandidateViewModel>>(candidates);
        return View(candidateViewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
        {
            return NotFound();
        }

        var candidateViewModel = _mapper.Map<CandidateViewModel>(candidate);
        return View(candidateViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CandidateViewModel candidateViewModel)
    {
        if (ModelState.IsValid)
        {
            var candidate = _mapper.Map<Candidate>(candidateViewModel);
            await _unitOfWork.Candidates.AddAsync(candidate);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(candidateViewModel);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
        {
            return NotFound();
        }

        var candidateViewModel = _mapper.Map<CandidateViewModel>(candidate);
        return View(candidateViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CandidateViewModel candidateViewModel)
    {
        if (id != candidateViewModel.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var candidate = _mapper.Map<Candidate>(candidateViewModel);
            await _unitOfWork.Candidates.UpdateAsync(candidate);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(candidateViewModel);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
        {
            return NotFound();
        }

        var candidateViewModel = _mapper.Map<CandidateViewModel>(candidate);
        return View(candidateViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate != null)
        {
            _unitOfWork.Candidates.Delete(candidate);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
