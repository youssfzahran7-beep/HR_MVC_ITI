using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HR_MVC_ITI.Controllers;

[Authorize(Roles = "HR")]
public class EmployeeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmployeeController(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        return View(employees);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    public async Task<IActionResult> Create()
    {
        await LoadUsers();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadUsers(model.UserId);
            return View(model);
        }

        var employee = _mapper.Map<Employee>(model);
        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ADD(EmployeeViewModel model)
    {
        return await Create(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        await LoadUsers(employee.UserId);
        return View(_mapper.Map<EmployeeViewModel>(employee));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await LoadUsers(model.UserId);
            return View(model);
        }

        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        _mapper.Map(model, employee);
        await _unitOfWork.Employees.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee != null)
        {
            // Delete related records first (FK = NoAction)
            var attendances = (await _unitOfWork.Attendances.GetAllAsync())
                .Where(a => a.EmployeeId == id).ToList();
            foreach (var a in attendances)
                _unitOfWork.Attendances.Delete(a);

            var payrolls = (await _unitOfWork.Payrolls.GetAllAsync())
                .Where(p => p.EmployeeId == id).ToList();
            foreach (var p in payrolls)
                _unitOfWork.Payrolls.Delete(p);

            var contracts = (await _unitOfWork.Contracts.GetAllAsync())
                .Where(c => c.EmployeeId == id).ToList();
            foreach (var c in contracts)
                _unitOfWork.Contracts.Delete(c);

            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadUsers(string? selectedUserId = null)
    {
        var users = await _userManager.Users.ToListAsync();
        ViewBag.Users = new SelectList(users, "Id", "Email", selectedUserId);
    }
}
