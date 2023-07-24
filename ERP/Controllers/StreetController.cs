using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Controllers
{
    public class StreetController : Controller
    {
        private readonly IStreetService _StreetService;
        public StreetController(IStreetService StreetService)
        {
            _StreetService = StreetService;
        }
        public IActionResult Index()
        {
            List<Street> _listOfStreets = new List<Street>();            
            _listOfStreets = _StreetService.GetStreetWithBlockAndSectors();
            return View(_listOfStreets);
        }
        public IActionResult Create()
        {            
            List<Sector> _listOfSectors = _StreetService.GetSectors();
            ViewBag.SectorsList = new SelectList(_listOfSectors, "SectorId", "SectorName");
            List<Block> _listOfBlocks = _StreetService.GetBlocks();
            ViewBag.BlocksList = new SelectList(_listOfBlocks, "BlockId", "BlockName");
           
            return View();
           
        }
        [HttpPost]
        public IActionResult Create(Street street)
        {
            if (ModelState.IsValid)
            {
                _StreetService.AddStreet(street);
                return RedirectToAction("Index");
            }
            List<Sector> _listOfSectors = _StreetService.GetSectors();
            ViewBag.SectorsList = new SelectList(_listOfSectors, "SectorId", "SectorName");        
            List<Block> _listOfBlocks = _StreetService.GetBlocks();
            ViewBag.BlocksList = new SelectList(_listOfBlocks, "BlockId", "BlockName");
            return View(street);
        }

        [HttpPost]
        public JsonResult GetBlocksBySector(int sectorId)
        {
            List<Block> blocks = _StreetService.GetBlockWithSector(sectorId);
            var blockList = blocks.Select(b => new { value = b.BlockId, text = b.BlockName }).ToList();
            return Json(blockList);
        }

        public IActionResult Edit(int id)
        {
            Street street = _StreetService.GetStreetByID(id);
            List<Block> _listOfBlocks = _StreetService.GetBlocks();
            string sectorName = _StreetService.GetSectorName(id);
            ViewBag.sector = sectorName;
            ViewBag.BlocksList = new SelectList(_listOfBlocks, "BlockId", "BlockName");
            return View(street);
        }
        [HttpPost]
        public IActionResult Edit(Street street)
        {
            _StreetService.UpdateStreet(street);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _StreetService.DeleteStreet(id);
            return RedirectToAction("Index");
        }
    }
}
