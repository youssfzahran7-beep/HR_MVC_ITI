using AutoMapper;
using HR_MVC_ITI.Models.Enitityes;
using HR_MVC_ITI.Models.IRepository;
using HR_MVC_ITI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR_MVC_ITI.Controllers
{
    [Authorize(Roles = "HR")]
    public class ContractController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContractController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contracts = await _unitOfWork.Contracts.GetAllAsync(c => c.Employee!);
            var contractVms = _mapper.Map<IEnumerable<ContractViewModel>>(contracts);
            return View(contractVms);
        }

        public async Task<IActionResult> Create()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContractViewModel contractVm)
        {
            if (ModelState.IsValid)
            {
                var contract = _mapper.Map<Contract>(contractVm);
                await _unitOfWork.Contracts.AddAsync(contract);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var employees = await _unitOfWork.Employees.GetAllAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "FullName", contractVm.EmployeeId);
            return View(contractVm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contract = await _unitOfWork.Contracts.GetByIdAsync(id, c => c.Employee!);
            if (contract == null)
            {
                return NotFound();
            }
            var contractVm = _mapper.Map<ContractViewModel>(contract);
            var employees = await _unitOfWork.Employees.GetAllAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "FullName", contractVm.EmployeeId);
            return View(contractVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContractViewModel contractVm)
        {
            if (ModelState.IsValid)
            {
                var contract = _mapper.Map<Contract>(contractVm);
                await _unitOfWork.Contracts.UpdateAsync(contract);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var employees = await _unitOfWork.Employees.GetAllAsync();
            ViewBag.Employees = new SelectList(employees, "Id", "FullName", contractVm.EmployeeId);
            return View(contractVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contract = await _unitOfWork.Contracts.GetByIdAsync(id, c => c.Employee!);
            if (contract == null)
            {
                return NotFound();
            }
            var contractVm = _mapper.Map<ContractViewModel>(contract);
            return View(contractVm);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _unitOfWork.Contracts.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            _unitOfWork.Contracts.Delete(contract);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}