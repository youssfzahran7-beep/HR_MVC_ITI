using HR_MVC_ITI.Models.Enitityes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_MVC_ITI.Controllers
{
    public class CandidateController : Controller
    {
        // 1. Displays the list of candidate profiles
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 2. Retrieves specific candidate profile details
        //    along with their linked user account
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }

        // 3. Renders the candidate profile creation form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 4. Saves candidate profile information
        //    and handles resume file upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Candidate candidate, IFormFile resumeFile)
        {
            if (!ModelState.IsValid)
            {
                return View(candidate);
            }

            // Resume file saving will be handled here
            // when the data/file layer is connected.

            // Candidate saving will be handled by the data-layer teammate.

            return RedirectToAction(nameof(Index));
        }

        // 5. Renders the edit form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        // 6. Submits updates to candidate profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Candidate candidate, IFormFile? resumeFile)
        {
            if (!ModelState.IsValid)
            {
                return View(candidate);
            }

            // Candidate updating will be handled by the data-layer teammate.
            // Resume replacement will also be handled when connected.

            return RedirectToAction(nameof(Index));
        }

        // 7. Deletes a candidate profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Delete will be handled by the data-layer teammate.

            return RedirectToAction(nameof(Index));
        }
    }
}
