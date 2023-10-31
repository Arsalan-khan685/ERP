using ERP.Models;
using ERP.Models.ViewModels;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Transactions;

namespace ERP.Controllers
{
    public class PlotController : Controller
    {
        private readonly IPlotService _plotservices;
        public PlotController(IPlotService plotService) 
        {
            _plotservices = plotService;
        }
        public IActionResult Index()
        {
            List<PlotViewModel> plots = new List<PlotViewModel>();
            plots = _plotservices.GetPlots();
            return View(plots);
        }

        public IActionResult Create()
        {
            
            List<Sector> _listOfSectors = _plotservices.GetSectors();
            ViewBag.Sectors = new SelectList(_listOfSectors, "SectorId", "SectorName");
            //List <Block> _listOfBlocks = _plotservices.GetBlocks();
            //ViewBag.Blocks = new SelectList(_listOfBlocks, "BlockId", "BlockName");
            //List<Street> _listOfStreets = _plotservices.GetStreets();
            //ViewBag.Streets = new SelectList(_listOfStreets, "StreetId", "StreetName");
            List<PlotType> _listOfPlotTypes = _plotservices.GetPlotTypes();
            ViewBag.PlotTypes = new SelectList(_listOfPlotTypes, "PlotTypeId", "PlotTypeName");
            List<PlotSize> _listOfPlotSizes = _plotservices.GetPlotSizes();
            ViewBag.PlotSizes = new SelectList(_listOfPlotSizes, "PlotSizeId", "Plot_Size");

            return View();
        }
        [HttpPost]
        public IActionResult Create(PlotViewModel plot)
        {           
                _plotservices.AddPlot(plot);
                return RedirectToAction("Index");                       
        }
       
        [HttpPost]
        public JsonResult GetBlocksBySector(int sectorId)
        {
            List<Block> blocks = _plotservices.GetBlockWithSector(sectorId);
            var blockList = blocks.Select(b => new { value = b.BlockId, text = b.BlockName }).ToList();
            return Json(blockList);
        }
        [HttpPost]
        public JsonResult GetStreetsByBlock(int blockId)
        {
            List<Street> streets = _plotservices.GetStreetsWithBlock(blockId);
            var streetsList = streets.Select(b => new { value = b.StreetId, text = b.StreetName }).ToList();
            return Json(streetsList);
        }

        public IActionResult Edit(int id)
        {
            PlotViewModel plot = _plotservices.GetPlotById(id);
            string sector = _plotservices.GetSectorName(id);
            ViewBag.Sector = sector;
            Block block = _plotservices.GetBlockNameandId(id);
            ViewBag.block = block;
            List<Street> _listofStreet = _plotservices.GetStreetsWithBlock(block.BlockId);            
            ViewBag.Street = new SelectList(_listofStreet, "StreetId", "StreetName");
            List<PlotType> _listofPlotTypes = _plotservices.GetPlotTypes();
            ViewBag.PlotTypes = new SelectList(_listofPlotTypes, "PlotTypeId", "PlotTypeName");
            List<PlotSize> _listofPlotSizes = _plotservices.GetPlotSizes();
            ViewBag.PlotSizes = new SelectList(_listofPlotSizes, "PlotSizeId", "Plot_Size");          
            return View(plot);
        }
        [HttpPost]
        public IActionResult Edit(Plot plot, int streetId, int plotTypeId, int plotSizeId)
        {
            plot.Street_Id = streetId;
            plot.PlotType_Id = plotTypeId;
            plot.PlotSize_Id = plotSizeId;
            if (ModelState.IsValid)
            {
                _plotservices.UpdatePlot(plot);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            _plotservices.DeletePlot(id);
            return RedirectToAction("Index");
        }
    }
}

