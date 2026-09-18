using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers;

[Authorize]
public class AttendanceController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public AttendanceController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    // ===== HR Actions =====

    [Authorize(Roles = "HR")]
    public async Task<IActionResult> Index()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
        return View("Add", employeeDtos);
    }

    [Authorize(Roles = "HR")]
    public async Task<IActionResult> GetAll()
    {
        var attendances = await _unitOfWork.Attendances.GetAllAsync();
        var attendanceDtos = _mapper.Map<List<AttendanceViewModel>>(attendances);

        var employees = (await _unitOfWork.Employees.GetAllAsync())
            .ToDictionary(e => e.Id, e => e.FullName);

        ViewBag.EmployeeNames = employees;

        return View(attendanceDtos);
    }

    // HR can check in any employee by id
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> AddAttendance(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        await PerformCheckIn(employee);
        return RedirectToAction(nameof(Index));
    }

    // HR can check out any employee by id
    [Authorize(Roles = "HR")]
    public async Task<IActionResult> CheckOut(int id)
    {
        await PerformCheckOut(id);
        return RedirectToAction(nameof(GetAll));
    }

    // ===== Employee Actions =====

    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> MyAttendance()
    {
        var employee = await GetCurrentEmployee();
        if (employee == null)
        {
            return NotFound("لم يتم ربط حسابك بسجل موظف.");
        }

        var allAttendances = await _unitOfWork.Attendances.GetAllAsync();
        var myAttendances = allAttendances
            .Where(a => a.EmployeeId == employee.Id)
            .OrderByDescending(a => a.Date)
            .ToList();

        var attendanceDtos = _mapper.Map<List<AttendanceViewModel>>(myAttendances);

        ViewBag.EmployeeName = employee.FullName;
        ViewBag.EmployeeId = employee.Id;

        // Check if already checked in today (no checkout yet)
        var todayRecord = myAttendances
            .FirstOrDefault(a => a.Date == DateTime.Today && a.CheckOutTime == null);
        ViewBag.HasOpenCheckIn = todayRecord != null;

        // Check if already checked in today at all
        var anyTodayRecord = myAttendances.Any(a => a.Date == DateTime.Today);
        ViewBag.AlreadyCheckedIn = anyTodayRecord;

        return View(attendanceDtos);
    }

    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> MyCheckIn()
    {
        var employee = await GetCurrentEmployee();
        if (employee == null)
        {
            return NotFound();
        }

        await PerformCheckIn(employee);
        return RedirectToAction(nameof(MyAttendance));
    }

    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> MyCheckOut()
    {
        var employee = await GetCurrentEmployee();
        if (employee == null)
        {
            return NotFound();
        }

        await PerformCheckOut(employee.Id);
        return RedirectToAction(nameof(MyAttendance));
    }

    // ===== Shared Logic =====

    private async Task PerformCheckIn(Employee employee)
    {
        var schedules = await _unitOfWork.WorkSchedules.GetAllAsync();
        var activeSchedule = schedules.FirstOrDefault(s => s.IsActive);

        var now = DateTime.Now;
        int lateMinutes = 0;

        if (activeSchedule != null)
        {
            var scheduledCheckIn = activeSchedule.CheckInTime;
            var actualCheckIn = now.TimeOfDay;

            if (actualCheckIn > scheduledCheckIn)
            {
                lateMinutes = (int)(actualCheckIn - scheduledCheckIn).TotalMinutes;
            }
        }

        var attendance = new Attendance
        {
            EmployeeId = employee.Id,
            Date = DateTime.Today,
            CheckInTime = now,
            LateMinutes = lateMinutes,
            OvertimeHours = 0
        };

        await _unitOfWork.Attendances.AddAsync(attendance);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task PerformCheckOut(int employeeId)
    {
        var allAttendances = await _unitOfWork.Attendances.GetAllAsync();
        var attendance = allAttendances
            .FirstOrDefault(a => a.EmployeeId == employeeId && a.Date == DateTime.Today && a.CheckOutTime == null);

        if (attendance == null)
        {
            return;
        }

        var schedules = await _unitOfWork.WorkSchedules.GetAllAsync();
        var activeSchedule = schedules.FirstOrDefault(s => s.IsActive);

        var now = DateTime.Now;
        attendance.CheckOutTime = now;

        if (activeSchedule != null)
        {
            var scheduledCheckOut = activeSchedule.CheckOutTime;
            var actualCheckOut = now.TimeOfDay;

            if (actualCheckOut > scheduledCheckOut)
            {
                attendance.OvertimeHours = (decimal)(actualCheckOut - scheduledCheckOut).TotalHours;
            }
        }

        await _unitOfWork.Attendances.UpdateAsync(attendance);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Employee?> GetCurrentEmployee()
    {
        var userId = _userManager.GetUserId(User);
        if (userId == null) return null;

        var employees = await _unitOfWork.Employees.GetAllAsync();
        return employees.FirstOrDefault(e => e.UserId == userId);
    }
}
