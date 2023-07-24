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
                _sectorService.AddSector(sector);
                return RedirectToAction("Index");
            }
            return View();
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
                _sectorService.UpdateSector(sector);
                return RedirectToAction("Index");
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
