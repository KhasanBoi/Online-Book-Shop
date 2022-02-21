using DataLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelsLayer;
using ModelsLayer.ViewModels;

namespace Online_Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductController(IUnitofWork _unitofWork, IWebHostEnvironment _hostEnvironment)
        {
            unitofWork = _unitofWork;
            hostEnvironment = _hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }


        // Get
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                Product = new(),
                CategoryList = unitofWork.Category.GetAll().Select(u => new SelectListItem  
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = unitofWork.CoverType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {

                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = unitofWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productViewModel);
            }
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImageUrl = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImageUrl))
                        {
                            System.IO.File.Delete(oldImageUrl);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if(obj.Product.Id == 0)
                {
                    unitofWork.Product.Add(obj.Product);
                }
                else
                {
                    unitofWork.Product.Update(obj.Product);
                }
                unitofWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region Api Class
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = unitofWork.Product.GetAll(includeProps: "Category,CoverType");
            return Json(new { data = productList });
        }

        // Post
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            var obj = unitofWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            var oldImageUrl = Path.Combine(hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImageUrl))
            {
                System.IO.File.Delete(oldImageUrl);
            }
            unitofWork.Product.Remove(obj);
            unitofWork.Save();
            return RedirectToAction("Index");
        }
        #endregion
    }
}