using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class SectorController : Controller
    {
        private readonly ISectorService _sectorService;
        public SectorController(ISectorService sectorService) 
        {
            _sectorService = sectorService;
        }

        public IActionResult Index()
        {
            List<Sector> _listOfSectors = new List<Sector>();
            _listOfSectors = _sectorService.GetSectors();
            return View(_listOfSectors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sector sector)
        {
            if (ModelState.IsValid)
            {
                string res = _sectorService.AddSector(sector);
                if(res == null)
                {
                    TempData["message"] = "Sector Added Succesfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = res;
                    return View(sector);
                }
            }
            return View(sector);
        }
        public IActionResult Edit(int id) 
        {
            Sector sector = new Sector();
            sector = _sectorService.GetSectorByID(id);
            return View(sector);
        }
        [HttpPost]
        public IActionResult Edit(Sector sector)
        {
            if (ModelState.IsValid)
            {
                string res = _sectorService.UpdateSector(sector);
                if (res == null)
                {
                    TempData["message"] = res;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = res;
                    return View(sector);
                }
            }
            return View();            
        }
        public IActionResult Delete(int id) 
        {
            _sectorService.DeleteSector(id);
            return RedirectToAction("Index");
        }

    }
}
