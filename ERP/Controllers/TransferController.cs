using ERP.Models;
using ERP.Models.ViewModels;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Controllers
{
    public class TransferController : Controller
    {
        public ITransferService _transferService { get; set; }
        public ISectorService _sectorService { get; set; }
        public IRelationService _relationService { get; set; } 
        public TransferController(ITransferService transferService, ISectorService sectorService,IRelationService relationService)
        {
            _transferService = transferService;
            _sectorService = sectorService;
            _relationService = relationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            List<Sector> _listofSector = _sectorService.GetSectors();
            ViewBag.Sectors = new SelectList(_listofSector, "SectorId", "SectorName");
            List<Relation> _listofRelations = _relationService.GetRelations();
            ViewBag.Relations = new SelectList(_listofRelations, "RelationId", "RelationName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(TransferViewModel model)
        {
            ModelState.AddModelError("CustomError", "This is a custom error.");

            if (ModelState.IsValid)
            {
                _transferService.AddTransferInformation(model);
                ModelState.Clear();
                model.Clients.Name = string.Empty;
                model.Clients.GuardianName = string.Empty; ;
                model.Clients.CNIC = string.Empty; ;
                model.Clients.PassportNo = string.Empty;
                model.Clients.MailingAddress = string.Empty;
                model.Clients.PermanentAddress = string.Empty;
                model.Clients.PhoneNo = string.Empty;
                model.Clients.Email = string.Empty;
                model.Nominees.NomineeName = string.Empty;
                model.Nominees.NomineeCNIC = string.Empty;
                model.Nominees.NomineeGuardian = string.Empty;
                return View(model);
            }
            return View(model);
        }
        [HttpPost]
        public JsonResult GetBlocksBySector(int sectorId)
        {
            List<Block> blocks = _transferService.GetBlockWithSector(sectorId);
            var blockList = blocks.Select(b => new { value = b.BlockId, text = b.BlockName }).ToList();
            return Json(blockList);
        }
        [HttpPost]
        public JsonResult GetStreetsByBlock(int blockId)
        {
            List<Street> streets = _transferService.GetStreetsWithBlock(blockId);
            var streetsList = streets.Select(b => new { value = b.StreetId, text = b.StreetName }).ToList();
            return Json(streetsList);
        }
        [HttpPost]
        public JsonResult GetPlotByStreet(int streetId)
        {
            List<Plot> plots = _transferService.GetPlotsWithStreet(streetId);
            var plotsList = plots.Select(b => new { value = b.PlotId, text = b.PlotNo }).ToList();
            return Json(plotsList);
        }
        [HttpGet]
        public JsonResult GetPlotDetails(int plotId)
        {
            var plotDetails = _transferService.GetPlotDetails(plotId);
            return Json(plotDetails);
        }
    }
}
