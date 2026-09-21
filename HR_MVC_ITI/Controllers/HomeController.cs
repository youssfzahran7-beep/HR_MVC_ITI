using HR_MVC_ITI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HR_MVC_ITI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly HR_MVC_ITI.Models.IRepository.IUnitOfWork _unitOfWork;

        public HomeController(HR_MVC_ITI.Models.IRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            ViewBag.EmployeeCount = employees.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
