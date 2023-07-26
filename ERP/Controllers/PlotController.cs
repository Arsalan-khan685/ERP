using ERP.Models;
using ERP.Models.ViewModels;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            List <Block> _listOfBlocks = _plotservices.GetBlocks();
            ViewBag.Blocks = new SelectList(_listOfBlocks, "BlockId", "BlockName");
            List<Street> _listOfStreets = _plotservices.GetStreets();
            ViewBag.Streets = new SelectList(_listOfStreets, "StreetId", "StreetName");
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
            
            //List<Sector> _listOfSectors = _plotservices.GetSectors();
            //ViewBag.Sectors = new SelectList(_listOfSectors, "SectorId", "SectorName");
            //List<Block> _listOfBlocks = _plotservices.GetBlocks();
            //ViewBag.Blocks = new SelectList(_listOfBlocks, "BlockId", "BlockName");
            //List<Street> _listOfStreets = _plotservices.GetStreets();
            //ViewBag.Streets = new SelectList(_listOfStreets, "StreetId", "StreetName");
            //List<PlotType> _listOfPlotTypes = _plotservices.GetPlotTypes();
            //ViewBag.PlotTypes = new SelectList(_listOfPlotTypes, "PlotTypeId", "PlotTypeName");
            //List<PlotSize> _listOfPlotSizes = _plotservices.GetPlotSizes();
            //ViewBag.PlotSizes = new SelectList(_listOfPlotSizes, "PlotSizeId", "Plot_Size");


            //plot.SectorName = GetItemNameById(plot.SectorId, ViewBag.Sectors);
            //plot.BlockName = GetItemNameById(plot.BlockId, ViewBag.Blocks);
            //plot.StreetName = GetItemNameById(plot.StreetId, ViewBag.Streets);
            //plot.PlotTypeName = GetItemNameById(plot.PlotTypeId, ViewBag.PlotTypes);
            //plot.Plot_Size = GetItemNameById(plot.PlotSizeId, ViewBag.PlotSizes);

        }

        //private string GetItemNameById(int id, SelectList list)
        //{
        //    var selectedItem = list.FirstOrDefault(item => item.Value == id.ToString());
        //    return selectedItem != null ? selectedItem.Text : string.Empty;
        //}
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

    }
}

