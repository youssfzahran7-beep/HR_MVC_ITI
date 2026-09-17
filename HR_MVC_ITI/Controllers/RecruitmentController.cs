using AutoMapper;
using HR_MVC_ITI.DTOs;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
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
        var recruitmentDtos = _mapper.Map<IEnumerable<RecruitmentDTO>>(recruitments);
        return View(recruitmentDtos);
    }

    public async Task<IActionResult> Details(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment == null)
        {
            return NotFound();
        }
        var recruitmentDto = _mapper.Map<RecruitmentDTO>(recruitment);
        return View(recruitmentDto  );
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RecruitmentDTO recruitmentDto)
    {
        if (ModelState.IsValid)
        {
            var recruitment = _mapper.Map<Recruitment>(recruitmentDto);
            await _unitOfWork.Recruitments.AddAsync(recruitment);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(recruitmentDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment == null)
        {
            return NotFound();
        }
        var recruitmentDto = _mapper.Map<RecruitmentDTO>(recruitment);
        return View(recruitmentDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RecruitmentDTO recruitmentDto)
    {
        if (id != recruitmentDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var recruitment = _mapper.Map<Recruitment>(recruitmentDto);
            await _unitOfWork.Recruitments.UpdateAsync(recruitment);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(recruitmentDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var recruitment = await _unitOfWork.Recruitments.GetByIdAsync(id);

        if (recruitment == null)
        {
            return NotFound();
        }
        var recruitmentDto = _mapper.Map<RecruitmentDTO>(recruitment);
        return View(recruitmentDto);
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
