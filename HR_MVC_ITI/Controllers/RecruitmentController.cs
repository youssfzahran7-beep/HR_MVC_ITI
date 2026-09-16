using HR_MVC_ITI.Models.Enitityes;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers
{
    public class RecruitmentController : Controller
    {
        // 1. Displays the list of all job postings
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 2. Retrieves specific job posting details
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }

        // 3. Renders the web page form to create a new job posting
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 4. Receives form submission to save a new job posting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Recruitment recruitment)
        {
            if (!ModelState.IsValid)
            {
                return View(recruitment);
            }

            // Data saving will be handled by the data-layer teammate.

            return RedirectToAction(nameof(Index));
        }

        // 5. Renders the web page form populated with an existing job posting
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        // 6. Submits changes to update job posting details or status
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Recruitment recruitment)
        {
            if (!ModelState.IsValid)
            {
                return View(recruitment);
            }

            // Data updating will be handled by the data-layer teammate.

            return RedirectToAction(nameof(Index));
        }

        // 7. Removes or archives a job posting
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Delete/archive will be handled by the data-layer teammate.

            return RedirectToAction(nameof(Index));
        }
    }
}