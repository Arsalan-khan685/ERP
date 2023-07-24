using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Controllers
{
    public class BlockController : Controller
    {
        private readonly IBlockService _blockService;
        public BlockController(IBlockService blockService) 
        {
            _blockService = blockService;
        }
        public IActionResult Index()
        {
            List<Block> _listOfBlocks = new List<Block>();
            _listOfBlocks = _blockService.GetBlockWithSector();
            return View(_listOfBlocks);
        }
        public IActionResult Create()
        {
            List<Sector> _listOfSectors = _blockService.GetSectors();
            ViewBag.SectorsList = new SelectList(_listOfSectors, "SectorId", "SectorName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Block block)
        {
            if(ModelState.IsValid)
            {
                _blockService.AddBlock(block);
                return RedirectToAction("Index");
            }
            List<Sector> _listOfSectors = _blockService.GetSectors();
            ViewBag.SectorsList = new SelectList(_listOfSectors, "SectorId", "SectorName");
            return View(block);
        }
        
        public IActionResult Edit(int id)
        {
            Block block = _blockService.GetBlockByID(id);
            if(block == null)
            {
                return NotFound();
            }
            List<Sector> _listOfSectors = _blockService.GetSectors();
            ViewBag.SectorList = new SelectList(_listOfSectors, "SectorId", "SectorName");
            return View(block);
        }
        [HttpPost]
        public IActionResult Edit(Block block)
        {
            if(ModelState.IsValid)
            {
                _blockService.UpdateBlock(block);
                return RedirectToAction("Index");
            }
            List<Sector> _listOfSectors = _blockService.GetSectors();
            ViewBag.SectorList = new SelectList(_listOfSectors, "SectorId", "SectorName");
            return View(block);
        }
        public IActionResult Delete(int id)
        {
            _blockService.DeleteBlock(id);
            return RedirectToAction("Index");
        }
    }
}
