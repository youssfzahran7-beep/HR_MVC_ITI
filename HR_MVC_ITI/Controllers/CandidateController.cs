using AutoMapper;
using HR_MVC_ITI.DTOs;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
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
        var candidateDtos = _mapper.Map<IEnumerable<CandidateDTO>>(candidates);
        return View(candidateDtos);
    }

    public async Task<IActionResult> Details(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
        {
            return NotFound();
        }
        var candidateDto = _mapper.Map<CandidateDTO>(candidate);
        return View(candidateDto);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CandidateDTO candidateDto)
    {
        if (ModelState.IsValid)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);

            await _unitOfWork.Candidates.AddAsync(candidate);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(candidateDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
        {
            return NotFound();
        }
        var candidateDto = _mapper.Map<CandidateDTO>(candidate);
        return View(candidateDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CandidateDTO candidateDto)
    {
        if (id != candidateDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);

            await _unitOfWork.Candidates.UpdateAsync(candidate);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(candidateDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
        {
            return NotFound();
        }
        var candidateDto = _mapper.Map<CandidateDTO>(candidate);
        return View(candidateDto);
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
