using Microsoft.AspNetCore.Mvc;
using TestCoreApp.Models;
using TestCoreApp.Repository.Base;

namespace TestCoreApp.Controllers
{
    public class CategoriesController : Controller
    {
        public CategoriesController(IUnitofwork _myunit) 
        {
           myunit = _myunit;
        }
        //private Irepository<Category> _repository;
        private IUnitofwork myunit;


        public async Task< IActionResult> Index()
        {
            var onecat = myunit.categories.SelectOne(x => x.Name == "lap");
            var allCat = await myunit.categories.FindAllAsync("Items");
            return View( allCat);
        }

        public IActionResult New()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                myunit.categories.AddOne(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = myunit.categories.FindById(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                myunit.categories.UpdateOne(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = myunit.categories.FindById(Id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            myunit.categories.DeleteOne(category);
            TempData["successData"] = "category has been deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
