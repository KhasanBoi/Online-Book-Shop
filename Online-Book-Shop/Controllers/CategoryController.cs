using DataLayer.Data;
using DataLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.Models;

namespace Online_Book_Shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;

        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] // to prevent cross-side request forgery attack
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Invalid name or display order");
            }
            if(ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save(); // data goes to database and it saves all the changes
                TempData["success"] = "Category created successfully!";
                return Redirect("Index");
            }
            return View(obj);
            
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.GetFirstOrDefault(u => u.Id == id);
            if(category == null)
            {
                return NotFound();
            }

            return View(id);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] // to prevent cross-side request forgery attack
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Invalid name or display order");
            }
            if(ModelState.IsValid)
            {
                _db.Update(obj);
                _db.Save();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.GetFirstOrDefault(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // to prevent cross-side request forgery attack
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Remove(obj);
            _db.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
