using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_MVC_ITI.Controllers;

[Authorize(Roles = "HR")]
public class PayrollController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PayrollController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(GetAll));
    }

    public async Task<IActionResult> GetAll()
    {
        var payrolls = await _unitOfWork.Payrolls.GetAllAsync();
        var payrollViewModels = _mapper.Map<List<PayrollViewModel>>(payrolls);
        await PopulateEmployeeNames(payrollViewModels);

        return View(payrollViewModels);
    }

    public async Task<IActionResult> Add()
    {
        await LoadEmployees();
        return View(new PayrollViewModel
        {
            Month = DateTime.Today.Month,
            Year = DateTime.Today.Year
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SavePayroll(PayrollViewModel payrollViewModel)
    {
        if (!ModelState.IsValid)
        {
            await LoadEmployees(payrollViewModel.EmployeeId);
            return View("Add", payrollViewModel);
        }

        var payroll = _mapper.Map<Payroll>(payrollViewModel);
        await _unitOfWork.Payrolls.AddAsync(payroll);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(GetAll));
    }

    public async Task<IActionResult> Update(int id)
    {
        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);

        if (payroll == null)
        {
            return NotFound();
        }

        var payrollViewModel = _mapper.Map<PayrollViewModel>(payroll);
        await LoadEmployees(payrollViewModel.EmployeeId);

        return View(payrollViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveUpdatedPayroll(int id, PayrollViewModel payrollViewModel)
    {
        if (id != payrollViewModel.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await LoadEmployees(payrollViewModel.EmployeeId);
            return View("Update", payrollViewModel);
        }

        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);

        if (payroll == null)
        {
            return NotFound();
        }

        payroll.EmployeeId = payrollViewModel.EmployeeId;
        payroll.Month = payrollViewModel.Month;
        payroll.Year = payrollViewModel.Year;
        payroll.BasicSalary = payrollViewModel.BasicSalary;
        payroll.LateDeductions = payrollViewModel.LateDeductions;
        payroll.OvertimeAdditions = payrollViewModel.OvertimeAdditions;

        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(GetAll));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id);

        if (payroll == null)
        {
            return NotFound();
        }

        _unitOfWork.Payrolls.Delete(payroll);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(GetAll));
    }

    private async Task LoadEmployees(int? selectedEmployeeId = null)
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();

        ViewBag.Employees = new SelectList(
            employees.Select(employee => new { employee.Id, employee.FullName }),
            "Id",
            "FullName",
            selectedEmployeeId);
    }

    private async Task PopulateEmployeeNames(IEnumerable<PayrollViewModel> payrolls)
    {
        var employees = (await _unitOfWork.Employees.GetAllAsync())
            .ToDictionary(employee => employee.Id, employee => employee.FullName);

        foreach (var payroll in payrolls)
        {
            payroll.EmployeeName = employees.GetValueOrDefault(payroll.EmployeeId, "Unknown employee");
        }
    }
}
