using AutoMapper;
using HR_MVC_ITI.DTOs;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.Enumes;
using Microsoft.AspNetCore.Mvc;


namespace HR_MVC_ITI.Controllers;

public class ApplicationProcessController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ApplicationProcessController(IUnitOfWork unitOfWork, IMapper mapper  )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var applications = await _unitOfWork.ApplicationProcesses.GetAllAsync();
        var applicationDtos = _mapper.Map<IEnumerable<ApplicationProcessDTO>>(applications);
        return View(applicationDtos);
    }
    public async Task<IActionResult> Details(int id)
    {
        var application = await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

        if (application == null)
        {
            return NotFound();
        }
        var applicationDto = _mapper.Map<ApplicationProcessDTO>(application);
        return View(applicationDto);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ApplicationProcessDTO applicationDto)
    {
        if (ModelState.IsValid)
        {
            applicationDto.AppliedDate = DateTime.Now;
            applicationDto.CurrentStage = ApplicationStage.Applied;

            var applicationProcess =
                _mapper.Map<ApplicationProcess>(applicationDto);

            await _unitOfWork.ApplicationProcesses.AddAsync(applicationProcess);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(applicationDto);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int id,
    ApplicationProcessDTO applicationDto)
    {
        if (id != applicationDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var applicationProcess =
                await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

            if (applicationProcess == null)
            {
                return NotFound();
            }

            applicationProcess.CurrentStage = applicationDto.CurrentStage;

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(applicationDto);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var application = await _unitOfWork.ApplicationProcesses.GetByIdAsync(id);

        if (application == null)
        {
            return NotFound();
        }
        var applicationDto = _mapper.Map<ApplicationProcessDTO>(application);
        return View(applicationDto );
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
}