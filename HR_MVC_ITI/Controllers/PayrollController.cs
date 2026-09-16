using Microsoft.AspNetCore.Mvc;
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
    public class PayrollController : Controller
    {
        HRDbContext C;
        public PayrollController(HRDbContext c)
        {
            C = c;
        }
        public IActionResult Add()
        {
            var employees = C.Employees.ToList();
            return View("Add", employees);
        }
        public IActionResult SavePayroll(string employeename, int month,int year, decimal basicsalary,decimal latedeductions, decimal overtimeadditions)
        {
            Employee em = C.Employees.First(c => c.FullName == employeename);

            Payroll a = new Payroll() { EmployeeId = em.Id,Month = month, Year=year, BasicSalary=basicsalary, LateDeductions=latedeductions, OvertimeAdditions=overtimeadditions };
            C.Add(a);
            C.SaveChanges();
            return View("GetAll", C.Payrolls.ToList());
        }
        public IActionResult GetAll()
        {
            var payrolls = C.Payrolls.ToList();
            return View("GetAll", payrolls);
        }
        public IActionResult Update(int id)
        {
            var employees = C.Employees.ToList();
            ViewBag.Id = id;
            return View("Update", employees);
        }
        public IActionResult SaveUpdatedPayroll(int id, string employeename, int month, int year, decimal basicsalary, decimal latedeductions, decimal overtimeadditions)
        {
            Payroll I = C.Payrolls.First(e => e.Id == id);
            Employee em = C.Employees.First(c => c.FullName == employeename);
            I.EmployeeId = em.Id;
            I.Month = month;
            I.Year = year;
            I.BasicSalary = basicsalary;
            I.LateDeductions = latedeductions;
            I.OvertimeAdditions = overtimeadditions;
            return View("GetAll", C.Payrolls.ToList());
        }
        public IActionResult Delete(int id)
        {
            Payroll a = C.Payrolls.First(c => c.Id == id);
            C.Remove(a);
            C.SaveChanges();
            return View("GetAll", C.Payrolls.ToList());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
