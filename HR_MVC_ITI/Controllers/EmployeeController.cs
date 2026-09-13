using Application.HR.DTOs;
using Application.HR.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: Employee
    [HttpGet("Employee")]
    [HttpGet("Employee/Index")]
    public async Task<IActionResult> Index()
    {
        var employees = await _employeeService.GetAllAsync();
        return View(employees);
    }

    // GET: Employee/Details/5
    [HttpGet("Employee/Details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // GET: Employee/Create
    [HttpGet("Employee/Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    [HttpPost("Employee/Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create( EmployeeDTO employeeDto)
    {
        if (ModelState.IsValid)
        {
            await _employeeService.AddAsync(employeeDto);
            return RedirectToAction(nameof(Index));
        }
        return View(employeeDto);
    }

    // GET: Employee/Edit/5
    [HttpGet("Employee/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost("Employee/Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EmployeeDTO employeeDto)
    {
        if (id != employeeDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await _employeeService.UpdateAsync(employeeDto);
            return RedirectToAction(nameof(Index));
        }
        return View(employeeDto);
    }

    // GET: Employee/Delete/5
    [HttpGet("Employee/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost("Employee/Delete/{id}")]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
