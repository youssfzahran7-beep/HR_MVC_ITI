    using AutoMapper;
    using global::HR_MVC_ITI.Models.IRepository;
    using global::HR_MVC_ITI.Models.ViewModels;
    using HR_MVC_ITI.Models.Enitityes;
    using HR_MVC_ITI.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    namespace HR_MVC_ITI.Controllers
    {
        public class ContractController : Controller
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public ContractController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            // GET: Display list of Contract
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var contracts = await _unitOfWork.Contracts.GetAllAsync();
                var contractVms = _mapper.Map<IEnumerable<ContractViewModel>>(contracts);
                return View(contractVms);
            }

            // GET: Retrieves Details of Contract
            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
                if (contract == null) return NotFound();

                var contractVm = _mapper.Map<ContractViewModel>(contract);
                return View(contractVm);
            }

            // GET: Render Create of Application Contract
            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            // POST: Create Application Contract
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(ContractViewModel contractVm)
            {
                if (!ModelState.IsValid) return View(contractVm);

                var contractEntity = _mapper.Map<Contract>(contractVm);
                await _unitOfWork.Contracts.AddAsync(contractEntity);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // GET: Render Edit of Application contract
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
                if (contract == null) return NotFound();

                var contractVm = _mapper.Map<ContractViewModel>(contract);
                return View(contractVm);
            }

            // POST: Edit Application contract
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(ContractViewModel contractVm)
            {
                if (!ModelState.IsValid) return View(contractVm);

                var existingContract = await _unitOfWork.Contracts.GetByIdAsync(contractVm.Id);
                if (existingContract == null) return NotFound();

                _mapper.Map(contractVm, existingContract);
                await _unitOfWork.Contracts.UpdateAsync(existingContract);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // GET: Render Delete Confirmation
            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
                if (contract == null) return NotFound();

                var contractVm = _mapper.Map<ContractViewModel>(contract);
                return View(contractVm);
            }

            // POST: Delete of Contract
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
                if (contract == null) return NotFound();

                _unitOfWork.Contracts.Delete(contract);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
