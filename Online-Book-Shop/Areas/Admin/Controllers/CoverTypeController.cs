using DataLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer;

namespace Online_Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork unitofWork;
        public CoverTypeController(IUnitofWork _unitofWork)
        {
            unitofWork = _unitofWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = unitofWork.CoverType.GetAll(); 
            return View(coverTypes);
        }

        // Get
        public IActionResult Create()
        {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if(ModelState.IsValid)
            {
                unitofWork.CoverType.Add(obj);
                unitofWork.Save();
                TempData["success"] = "Cover Type created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var coverFromDb = unitofWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if(coverFromDb == null)
            {
                return NotFound();
            }
            return View(coverFromDb);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if(ModelState.IsValid)
            {
                unitofWork.CoverType.Update(obj);
                unitofWork.CoverType.Save();
                TempData["success"] = "Cover Type edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Get
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var coverFromDb = unitofWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if(coverFromDb == null)
            {
                return NotFound();
            }
            return View(coverFromDb);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = unitofWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if(obj == null)
            {
                return NotFound();
            }
            unitofWork.CoverType.Remove(obj);
            unitofWork.Save();
            TempData["success"] = "Cover Type removed successfully";
            return RedirectToAction("Index");
        }

    }
}
