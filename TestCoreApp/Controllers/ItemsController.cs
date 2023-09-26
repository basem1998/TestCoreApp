using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestCoreApp.Data;
using TestCoreApp.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TestCoreApp.Controllers
{
    public class ItemsController : Controller
    {
        public ItemsController(AppDbContext db , IHostingEnvironment host)
        {
            _db = db;
            _host = host;
        }   
        
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _host;
        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.Items.Include(a=>a.Category).ToList();
            return View(itemsList);
        }

        //GET
        public IActionResult New()
        {
            Selectitem();
            return View(); 
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                string filename = string.Empty;
                if (item.clientfile != null)
                {
                    string myupload = Path.Combine(_host.WebRootPath, "images");
                    filename = item.clientfile.FileName;
                    string fullpath = Path.Combine(myupload, filename);
                    item.clientfile.CopyTo(new FileStream(fullpath, FileMode.Create));
                    item.imagepath = filename;
                }
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["sucess"] = "item add";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }
        public void Selectitem(int selectid=1)
        {
            List<Category> categories = _db.categories.ToList();
            ViewBag.selectlist = new SelectList(categories, "Id", "Name",selectid);
        }
        //GET
        public IActionResult Edit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            Selectitem(item.CategoryId);
            return View(item);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Name == "100")
            {
                ModelState.AddModelError("Name", "Name can't equal 100");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["sucess"] = "item Edit";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        //GET
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            Selectitem(item.CategoryId);
            return View(item);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            _db.Remove(item);
           _db.SaveChanges();
            TempData["sucess"] = "item Delete";
            return RedirectToAction("Index");
        }
    }
}
