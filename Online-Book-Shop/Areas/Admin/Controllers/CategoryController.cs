using DataLayer.Data;
using DataLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.Models;

namespace Online_Book_Shop.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork unitofWork;

        public CategoryController(IUnitofWork _unitofWork)
        {
            unitofWork = _unitofWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = unitofWork.Category.GetAll();
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
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Invalid name or display order");
            }
            if (ModelState.IsValid)
            {
                unitofWork.Category.Add(obj);
                unitofWork.Save(); // data goes to database and it saves all the changes
                TempData["success"] = "Category created successfully!";
                return Redirect("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = unitofWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
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
            if (ModelState.IsValid)
            {
                unitofWork.Category.Update(obj);
                unitofWork.Save();
               // return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = unitofWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // to prevent cross-side request forgery attack
        public IActionResult DeletePost(int? id)
        {
            var obj = unitofWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            unitofWork.Category.Remove(obj);
            unitofWork.Save();
            return RedirectToAction("Index");
        }
    }
}