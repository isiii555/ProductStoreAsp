using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexAdmin()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult FilterByCategory(int categoryId)
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AddToCartProduct(int productId) 
        { 
            return View(); 
        }

        [Authorize(Roles = "User")]
        public IActionResult GetAllProducts()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddProduct(ProductViewModel addProductViewModel)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        public IActionResult EditProduct(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditProduct(int id, ProductViewModel editProductViewModel)
        {
            return View();
        }

    }
}
