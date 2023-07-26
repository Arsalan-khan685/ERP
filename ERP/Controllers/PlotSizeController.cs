using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class PlotSizeController : Controller
    {
        private readonly IPlotSizeService _PlotSizeService;
        public PlotSizeController(IPlotSizeService PlotSizeService)
        {
            _PlotSizeService = PlotSizeService;
        }
        public IActionResult Index()
        {
            List<PlotSize> _listOfPlotSizes = new List<PlotSize>();
            _listOfPlotSizes = _PlotSizeService.GetPlotSizes();
            return View(_listOfPlotSizes);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PlotSize plotSize)
        {
            _PlotSizeService.AddPlotSize(plotSize);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            PlotSize plotSize = _PlotSizeService.GetPlotSizeByID(id);
            return View(plotSize);
        }
        [HttpPost]
        public IActionResult Edit(PlotSize plotSize)
        {
            if(ModelState.IsValid)
            {
                _PlotSizeService.UpdatePlotSize(plotSize);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id) 
        {
            _PlotSizeService.DeletePlotSize(id);
            return RedirectToAction("Index");
        }
    }
}
