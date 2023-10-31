using ERP.Models;
using ERP.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Controllers
{
    public class RelationController : Controller
    {
        private readonly IRelationService _relationService;
        public RelationController(IRelationService relationService)
        {
            _relationService = relationService;
        }
        public IActionResult Index()
        {
            List<Relation> _listOfRelations = new List<Relation>();
            _listOfRelations = _relationService.GetRelations();
            return View(_listOfRelations);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Relation relation)
        {
            if (ModelState.IsValid)
            {
                _relationService.AddRelation(relation);
                return RedirectToAction("Index");
            }
            return View(relation);
        }
        public IActionResult Edit(int id)
        {
            Relation relation = _relationService.GetRelationByID(id);
            return View(relation);
        }
        [HttpPost]
        public IActionResult Edit(Relation relation)
        {
            if (ModelState.IsValid)
            {
                _relationService.UpdateRelation(relation);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            _relationService.DeleteRelation(id);
            return RedirectToAction("Index");
        }
    }

}
