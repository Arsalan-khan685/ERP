using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERP.Controllers
{
    public class PaymentPlanController : Controller
    {
        private readonly IPaymentPlanService _planService;
        public PaymentPlanController(IPaymentPlanService planService)
        {
                _planService = planService;
        }
        public IActionResult Index()
        {
            List<PaymentPlan> _listofPaymentPlans = _planService.GetAllPaymentPlans();
            return View(_listofPaymentPlans);
        }
        public IActionResult Create()
        {
            List<PlotType> _listofPlotTypes =  _planService.GetPlotTypes();
            ViewBag.PlotTypes = new SelectList(_listofPlotTypes, "PlotTypeId", "PlotTypeName");
            List<PlotSize> _listofPlotSize = _planService.GetPlotSizes();
            ViewBag.PlotSizes = new SelectList(_listofPlotSize, "PlotSizeId", "Plot_Size");
            List<InstallmentType> _listofInstallmentTypes = _planService.GetInstallmentTypes();
            ViewBag.InstallmentTypes = new SelectList(_listofInstallmentTypes, "InstallmentTypeId", "Installment_Type");
            return View();
        }
        [HttpPost]
        public IActionResult Create(PaymentPlan paymentPlan)
        {
            if(!ModelState.IsValid)
            {
                return View(paymentPlan);
            }
            else
            {
                _planService.AddPaymentPlan(paymentPlan);
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(int id)
        {
            List<PlotType> _listofPlotTypes = _planService.GetPlotTypes();
            ViewBag.PlotTypes = new SelectList(_listofPlotTypes, "PlotTypeId", "PlotTypeName");
            List<PlotSize> _listofPlotSize = _planService.GetPlotSizes();
            ViewBag.PlotSizes = new SelectList(_listofPlotSize, "PlotSizeId", "Plot_Size");
            List<InstallmentType> _listofInstallmentTypes = _planService.GetInstallmentTypes();
            ViewBag.InstallmentTypes = new SelectList(_listofInstallmentTypes, "InstallmentTypeId", "Installment_Type");
            PaymentPlan _paymentplan =  _planService.GetPaymentPlanById(id);
            return View(_paymentplan);
        }
        [HttpPost]
        public IActionResult Edit(PaymentPlan _paymentPlan)
        {
            if(ModelState.IsValid)
            {
                _planService.UpdatePaymentPlan(_paymentPlan);
                return RedirectToAction("Index");
            }
            return View(_paymentPlan);
        }
        public IActionResult Delete(int id)
        {
            _planService.DeletePaymentPlan(id);
            return RedirectToAction("Index");
        }
          
    }
}
