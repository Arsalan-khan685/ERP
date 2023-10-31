using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class InstallmentTypeController : Controller
    {
        private readonly IInstallmentTypeService _installmentTypeService;
        public InstallmentTypeController(IInstallmentTypeService installmentTypeService)
        {
            _installmentTypeService = installmentTypeService;
        }
        public IActionResult Index()
        {
            List<InstallmentType> _listOfInstallmentTypes = new List<InstallmentType>();
            _listOfInstallmentTypes = _installmentTypeService.GetInstallmentTypes();
            return View(_listOfInstallmentTypes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(InstallmentType installmentType)
        {
            _installmentTypeService.AddInstallmentType(installmentType);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            InstallmentType installmentType = _installmentTypeService.GetInstallmentTypeByID(id);
            return View(installmentType);
        }
        [HttpPost]
        public IActionResult Edit(InstallmentType installmentType)
        {
            if (ModelState.IsValid)
            {
                _installmentTypeService.UpdateInstallmentType(installmentType);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            _installmentTypeService.DeleteInstallmentType(id);
            return RedirectToAction("Index");
        }
    }
}
