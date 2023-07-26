using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class PlotTypeController : Controller
    {
        private readonly IPlotTypeService _plotTypeService;
        public PlotTypeController(IPlotTypeService plotTypeService)
        {
            _plotTypeService = plotTypeService;
        }
        public IActionResult Index()
        {
            List<PlotType> _listOfPlotTypes = new List<PlotType>();
            _listOfPlotTypes = _plotTypeService.GetPlotTypes();
            return View(_listOfPlotTypes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PlotType plotType)
        {
            _plotTypeService.AddPlotType(plotType);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            PlotType plotType = _plotTypeService.GetPlotTypeByID(id);
            return View(plotType);
        }
        [HttpPost]
        public IActionResult Edit(PlotType plotType)
        {
            if(ModelState.IsValid)
            {
                _plotTypeService.UpdatePlotType(plotType);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id) 
        {
            _plotTypeService.DeletePlotType(id);
            return RedirectToAction("Index");
        }
    }
}
