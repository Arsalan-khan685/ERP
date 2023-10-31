using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankService _bankService;
        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }
        public IActionResult Index()
        {
            List<Bank> _listOfBanks = new List<Bank>();
            _listOfBanks = _bankService.GetBanks();
            return View(_listOfBanks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Bank bank)
        {
            _bankService.AddBank(bank);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            Bank bank = _bankService.GetBankByID(id);
            return View(bank);
        }
        [HttpPost]
        public IActionResult Edit(Bank bank)
        {
            if (ModelState.IsValid)
            {
                _bankService.UpdateBank(bank);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            _bankService.DeleteBank(id);
            return RedirectToAction("Index");
        }
    }
}
