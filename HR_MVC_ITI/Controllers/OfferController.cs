using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.Enumes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace HR_MVC_ITI.Controllers
    {
        public class OfferController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public OfferController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            // 1. GET: Display List of Application Offers
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var offers = await _unitOfWork.Offers.GetAllAsync();
                var offerVms = _mapper.Map<IEnumerable<OfferViewModel>>(offers);
                return View(offerVms);
            }

            // 2. GET: Retrieves Details of Application Offer
            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var offer = await _unitOfWork.Offers.GetByIdAsync(id);
                if (offer == null)
                {
                    return NotFound();
                }

                var offerVm = _mapper.Map<OfferViewModel>(offer);
                return View(offerVm);
            }

            // 3. GET: Render Create Page
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // 4. POST: Save New Application Offer
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(OfferViewModel offerVm)
            {
                if (!ModelState.IsValid)
                {
                    return View(offerVm);
                }

                var offerEntity = _mapper.Map<ApplicationOffer>(offerVm);
                await _unitOfWork.Offers.AddAsync(offerEntity);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // 5. GET: Render Edit Page
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var offer = await _unitOfWork.Offers.GetByIdAsync(id);
                if (offer == null)
                {
                    return NotFound();
                }

                var offerVm = _mapper.Map<OfferViewModel>(offer);
                return View(offerVm);
            }

            // 6. POST: Save Edited Application Offer
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(OfferViewModel offerVm)
            {
                if (!ModelState.IsValid)
                {
                    return View(offerVm);
                }

                var existingOffer = await _unitOfWork.Offers.GetByIdAsync(offerVm.Id);
                if (existingOffer == null)
                {
                    return NotFound();
                }

                _mapper.Map(offerVm, existingOffer);
                await _unitOfWork.Offers.UpdateAsync(existingOffer);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // 7. GET: Render Delete Confirmation
            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                var offer = await _unitOfWork.Offers.GetByIdAsync(id);
                if (offer == null)
                {
                    return NotFound();
                }

                var offerVm = _mapper.Map<OfferViewModel>(offer);
                return View(offerVm);
            }

            // 8. POST: Delete Confirmed
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var offer = await _unitOfWork.Offers.GetByIdAsync(id);
                if (offer == null)
                {
                    return NotFound();
                }

                _unitOfWork.Offers.Delete(offer);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
