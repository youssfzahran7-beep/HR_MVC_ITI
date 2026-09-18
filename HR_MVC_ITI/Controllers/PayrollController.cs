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
  
        var payrolls = await _unitOfWork.Payrolls.GetAllAsync(p => p.Employee!);
        var payrollViewModels = _mapper.Map<List<PayrollViewModel>>(payrolls);

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

        // Auto-calculate from Attendance and Contract
        var attendances = await _unitOfWork.Attendances.GetAllAsync();
        var monthlyAttendances = attendances.Where(a => 
            a.EmployeeId == payrollViewModel.EmployeeId && 
            a.Date.Month == payrollViewModel.Month && 
            a.Date.Year == payrollViewModel.Year).ToList();

        var totalLateMinutes = monthlyAttendances.Sum(a => a.LateMinutes);
        var totalOvertimeHours = monthlyAttendances.Sum(a => a.OvertimeHours);

        var contracts = await _unitOfWork.Contracts.GetAllAsync();
        var activeContract = contracts.FirstOrDefault(c => c.EmployeeId == payrollViewModel.EmployeeId && c.Status == HR_MVC_ITI.Models.Enumes.ContractStatus.Activated) 
                             ?? contracts.LastOrDefault(c => c.EmployeeId == payrollViewModel.EmployeeId);

        decimal basicSalary = activeContract != null ? activeContract.BasicSalary : payrollViewModel.BasicSalary;
        
        // Assuming 30 days and 8 working hours per day
        decimal hourlyRate = basicSalary > 0 ? basicSalary / (30m * 8m) : 0;

        decimal calculatedLateDeductions = (decimal)totalLateMinutes / 60m * hourlyRate;
        decimal calculatedOvertimeAdditions = totalOvertimeHours * hourlyRate * 1.5m; // 1.5x for overtime

        payrollViewModel.BasicSalary = basicSalary;
        payrollViewModel.LateDeductions = Math.Round(calculatedLateDeductions, 2);
        payrollViewModel.OvertimeAdditions = Math.Round(calculatedOvertimeAdditions, 2);

        var payroll = _mapper.Map<Payroll>(payrollViewModel);
        await _unitOfWork.Payrolls.AddAsync(payroll);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(GetAll));
    }

    public async Task<IActionResult> Update(int id)
    {
        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id, p => p.Employee!);

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

        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id, p => p.Employee!);

        if (payroll == null)
        {
            return NotFound();
        }

        // Auto-calculate from Attendance and Contract
        var attendances = await _unitOfWork.Attendances.GetAllAsync();
        var monthlyAttendances = attendances.Where(a => 
            a.EmployeeId == payrollViewModel.EmployeeId && 
            a.Date.Month == payrollViewModel.Month && 
            a.Date.Year == payrollViewModel.Year).ToList();

        var totalLateMinutes = monthlyAttendances.Sum(a => a.LateMinutes);
        var totalOvertimeHours = monthlyAttendances.Sum(a => a.OvertimeHours);

        var contracts = await _unitOfWork.Contracts.GetAllAsync();
        var activeContract = contracts.FirstOrDefault(c => c.EmployeeId == payrollViewModel.EmployeeId && c.Status == HR_MVC_ITI.Models.Enumes.ContractStatus.Activated) 
                             ?? contracts.LastOrDefault(c => c.EmployeeId == payrollViewModel.EmployeeId);

        decimal basicSalary = activeContract != null ? activeContract.BasicSalary : payrollViewModel.BasicSalary;
        
        // Assuming 30 days and 8 working hours per day
        decimal hourlyRate = basicSalary > 0 ? basicSalary / (30m * 8m) : 0;

        decimal calculatedLateDeductions = (decimal)totalLateMinutes / 60m * hourlyRate;
        decimal calculatedOvertimeAdditions = totalOvertimeHours * hourlyRate * 1.5m; // 1.5x for overtime

        payrollViewModel.BasicSalary = basicSalary;
        payrollViewModel.LateDeductions = Math.Round(calculatedLateDeductions, 2);
        payrollViewModel.OvertimeAdditions = Math.Round(calculatedOvertimeAdditions, 2);

        _mapper.Map(payrollViewModel, payroll);
        await _unitOfWork.Payrolls.UpdateAsync(payroll);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToAction(nameof(GetAll));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var payroll = await _unitOfWork.Payrolls.GetByIdAsync(id, p => p.Employee!);

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
}
