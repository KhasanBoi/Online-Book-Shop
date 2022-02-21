using DataLayer.Migrations;
using DataLayer.Repository;
using DataLayer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Online_Book_Shop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWork unitofWork;

        public HomeController(ILogger<HomeController> logger, IUnitofWork _unitofWork)
        {
            _logger = logger;
            unitofWork = _unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ModelsLayer.Product> productList = unitofWork.Product.GetAll(includeProps: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            ModelsLayer.Product product = unitofWork.Product.GetFirstOrDefault(u=> u.Id==id, includeProps: "Category,CoverType");
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ModelsLayer.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}