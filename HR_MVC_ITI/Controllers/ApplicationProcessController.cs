using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.Enumes;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers
{
    public class ApplicationProcessController : Controller
    {
        // 1. Displays the active application tracking pipeline
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 2. Retrieves tracking details for a single application
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }

        // 3. Renders the job application form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 4. Creates a new application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationProcess applicationProcess)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationProcess);
            }

            // Data layer will handle saving.
            // CurrentStage = ApplicationStage.Applied
            // AppliedDate = DateTime.Now

            return RedirectToAction(nameof(Index));
        }

        // 5. Updates the application stage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStage(int id, ApplicationStage stage)
        {
            // Data layer will handle updating the stage.

            return RedirectToAction(nameof(Index));
        }

        // 6. Cancels/removes an application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Data layer will handle deletion/cancellation.

            return RedirectToAction(nameof(Index));
        }
    }
}