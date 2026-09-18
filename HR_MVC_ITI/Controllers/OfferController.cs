using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_MVC_ITI.Controllers
{
    [Authorize(Roles = "HR")]
    public class OfferController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OfferController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: /Offer
        public async Task<IActionResult> Index()
        {
            var offers = await _unitOfWork.Offers.GetAllAsync(o => o.ApplicationProcess!, o => o.ApplicationInterview!);
            var offerVms = _mapper.Map<IEnumerable<OfferViewModel>>(offers);
            return View(offerVms);
        }

        // GET: /Offer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdAsync(id, o => o.ApplicationProcess!, o => o.ApplicationInterview!);
            if (offer == null) return NotFound();

            var vm = _mapper.Map<OfferViewModel>(offer);
            return View(vm);
        }

        // GET: /Offer/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateSelectLists();
            return View();
        }

        // POST: /Offer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferViewModel offerVm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectLists(offerVm.ApplicationProcessId, offerVm.InterviewId);
                return View(offerVm);
            }

            var offer = _mapper.Map<ApplicationOffer>(offerVm);
            await _unitOfWork.Offers.AddAsync(offer);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Offer/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdAsync(id, o => o.ApplicationProcess!, o => o.ApplicationInterview!);
            if (offer == null) return NotFound();

            var vm = _mapper.Map<OfferViewModel>(offer);
            await PopulateSelectLists(vm.ApplicationProcessId, vm.InterviewId);
            return View(vm);
        }

        // POST: /Offer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfferViewModel offerVm)
        {
            if (id != offerVm.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopulateSelectLists(offerVm.ApplicationProcessId, offerVm.InterviewId);
                return View(offerVm);
            }

            var existing = await _unitOfWork.Offers.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(offerVm, existing);
           await _unitOfWork.Offers.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdAsync(id, o => o.ApplicationProcess!, o => o.ApplicationInterview!);
            if (offer == null) return NotFound();

            var vm = _mapper.Map<OfferViewModel>(offer);
            return View(vm);
        }

        // POST: /Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _unitOfWork.Offers.GetByIdAsync(id);
            if (offer == null) return NotFound();

            _unitOfWork.Offers.Delete(offer);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        private async Task PopulateSelectLists(int? selectedApplicationProcessId = null, int? selectedInterviewId = null)
        {
            // Application processes (include Candidate if your repo supports include)
            var applications = await _unitOfWork.ApplicationProcesses.GetAllAsync(a => a.Candidate!);
            var appItems = applications.Select(a => new
            {
                a.Id,
                Name = a.Candidate != null
                    ? $"{a.Candidate.FirstName} {a.Candidate.LastName}"
                    : $"App #{a.Id}"
            });
            ViewBag.ApplicationProcesses = new SelectList(appItems, "Id", "Name", selectedApplicationProcessId);

            // Interviews (show candidate name if available)
            var interviews = await _unitOfWork.Interviews.GetAllAsync(i => i.ApplicationProcess!);
            var interviewItems = interviews.Select(i => new
            {
                i.Id,
                Name = i.ApplicationProcess != null
                    ? $"App #{i.ApplicationProcess.Id} - Intv #{i.Id}"
                    : $"Interview #{i.Id}"
            });
            ViewBag.Interviews = new SelectList(interviewItems, "Id", "Name", selectedInterviewId);
        }
    }
}