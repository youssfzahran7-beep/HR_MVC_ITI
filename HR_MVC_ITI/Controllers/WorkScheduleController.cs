using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers;

[Authorize(Roles = "HR")]
public class WorkScheduleController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WorkScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var schedules = await _unitOfWork.WorkSchedules.GetAllAsync();
        var viewModels = _mapper.Map<List<WorkScheduleViewModel>>(schedules);
        return View(viewModels);
    }

    public IActionResult Create()
    {
        return View(new WorkScheduleViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WorkScheduleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        // Deactivate all existing schedules when creating a new active one
        if (viewModel.IsActive)
        {
            var existing = await _unitOfWork.WorkSchedules.GetAllAsync();
            foreach (var schedule in existing.Where(s => s.IsActive))
            {
                schedule.IsActive = false;
            }
        }

        var workSchedule = _mapper.Map<WorkSchedule>(viewModel);
        await _unitOfWork.WorkSchedules.AddAsync(workSchedule);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(id);
        if (schedule == null)
        {
            return NotFound();
        }

        var viewModel = _mapper.Map<WorkScheduleViewModel>(schedule);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WorkScheduleViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(id);
        if (schedule == null)
        {
            return NotFound();
        }

        // Deactivate others if this one is being set active
        if (viewModel.IsActive)
        {
            var existing = await _unitOfWork.WorkSchedules.GetAllAsync();
            foreach (var s in existing.Where(s => s.IsActive && s.Id != id))
            {
                s.IsActive = false;
            }
        }

        schedule.CheckInTime = viewModel.CheckInTime;
        schedule.CheckOutTime = viewModel.CheckOutTime;
        schedule.IsActive = viewModel.IsActive;

        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var schedule = await _unitOfWork.WorkSchedules.GetByIdAsync(id);
        if (schedule == null)
        {
            return NotFound();
        }

        _unitOfWork.WorkSchedules.Delete(schedule);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
