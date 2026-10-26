using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using HR_MVC_ITI.ViewModels;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_MVC_ITI.Controllers
{
    [Authorize(Roles = "HR")]
    public class InterviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InterviewController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // 1. GET: Display List of Interviews
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var interviews = await _unitOfWork.Interviews.GetAllAsync(
                i => i.ApplicationProcess!, 
                i => i.ApplicationProcess!.Candidate!);
            var interviewVms = _mapper.Map<IEnumerable<InterviewViewModel>>(interviews);
            return View(interviewVms);
        }

        // 2. GET: Retrieve Specific Interview Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var interview = await _unitOfWork.Interviews.GetByIdAsync(id, 
                i => i.ApplicationProcess!, 
                i => i.ApplicationProcess!.Candidate!);
            if (interview == null)
            {
                return NotFound();
            }

            var interviewVm = _mapper.Map<InterviewViewModel>(interview);
            return View(interviewVm);
        }

        // 3. GET: Render Create Page
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadApplicationProcesses();
            return View(new InterviewViewModel());
        }

        // 4. POST: Save New Interview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InterviewViewModel interviewVm)
        {
            if (!ModelState.IsValid)
            {
                await LoadApplicationProcesses(interviewVm.ApplicationProcessId);
                return View(interviewVm);
            }

            var interviewEntity = _mapper.Map<ApplicationInterview>(interviewVm);
            await _unitOfWork.Interviews.AddAsync(interviewEntity);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 5. GET: Render Edit Page
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var interview = await _unitOfWork.Interviews.GetByIdAsync(id, i => i.ApplicationProcess!);
            if (interview == null)
            {
                return NotFound();
            }

            await LoadApplicationProcesses(interview.ApplicationProcessId);
            var interviewVm = _mapper.Map<InterviewViewModel>(interview);
            return View(interviewVm);
        }

        // 6. POST: Save Edited Interview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InterviewViewModel interviewVm)
        {
            if (id != interviewVm.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                await LoadApplicationProcesses(interviewVm.ApplicationProcessId);
                return View(interviewVm);
            }

            var existingInterview = await _unitOfWork.Interviews.GetByIdAsync(id);
            if (existingInterview == null)
            {
                return NotFound();
            }

            _mapper.Map(interviewVm, existingInterview);
            await _unitOfWork.Interviews.UpdateAsync(existingInterview);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // 7. POST: Delete Interview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var interview = await _unitOfWork.Interviews.GetByIdAsync(id, i => i.ApplicationProcess!);
            if (interview == null)
            {
                return NotFound();
            }

            _unitOfWork.Interviews.Delete(interview);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadApplicationProcesses(int? selectedId = null)
        {
            var processes = await _unitOfWork.ApplicationProcesses.GetAllAsync(p => p.Candidate!, p => p.Recruitment!);
            ViewBag.ApplicationProcesses = new SelectList(
                processes.Select(p => new
                {
                    p.Id,
                    Name = $"Application #{p.Id} - {(p.Candidate != null ? p.Candidate.FirstName + " " + p.Candidate.LastName : "Candidate")} ({(p.Recruitment != null ? p.Recruitment.Title : "Job")})"
                }),
                "Id",
                "Name",
                selectedId);
        }
    }
}
