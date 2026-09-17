using AutoMapper;
using HR_MVC_ITI.Data;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HR_MVC_ITI.Controllers
{
    [Authorize(Roles = "HR")]
    public class AttendanceController : Controller
    {
        HRDbContext C;
        public AttendanceController(HRDbContext c)
        {
            C = c;
        }
        public IActionResult Add()
        {
            var employees = C.Employees.ToList();
            return View("Add",employees);
        }
        public IActionResult SaveAttendance(string employeename,DateTime date,DateTime checkintime,DateTime checkouttime,int lateminutes,decimal overtimehours)
        {
            Employee em = C.Employees.First(c => c.FullName == employeename);

            Attendance a = new Attendance() { EmployeeId = em.Id, Date = date, CheckInTime = checkintime, CheckOutTime = checkouttime, LateMinutes = lateminutes, OvertimeHours = overtimehours };
            C.Add(a);
            C.SaveChanges();
            return View("GetAll",C.Attendances.ToList());
         }
        public IActionResult GetAll()
        {
            var attendances= C.Attendances.ToList();
            return View("GetAll", attendances);
        }
        public IActionResult Update(int id)
        {
            var employees = C.Employees.ToList();
            ViewBag.Id = id;
            return View("Update",employees);
        }
        public IActionResult SaveUpdatedAttendance(int id,string employeename,DateTime date,DateTime checkintime,DateTime checkouttime,int lateminutes,decimal overtimrhours)
        {
            Attendance I = C.Attendances.First(e => e.Id == id);
            Employee em = C.Employees.First(c => c.FullName == employeename);
            I.EmployeeId = em.Id;
            I.Date = date;
            I.CheckInTime = checkintime;
            I.CheckOutTime = checkouttime;
            I.LateMinutes = lateminutes;
            I.OvertimeHours = overtimrhours;
            return View("GetAll", C.Attendances.ToList());
        }
        public IActionResult Delete(int id)
        {
            Attendance a= C.Attendances.First(c => c.Id == id);
            C.Remove(a);
            C.SaveChanges();
            return View("GetAll", C.Attendances.ToList());
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
