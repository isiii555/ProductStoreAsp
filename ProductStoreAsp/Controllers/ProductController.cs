using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ProductStoreAsp.Repositories.Abstractions;
using Ardalis.GuardClauses;
using System.Text.Json;

namespace ProductStoreAsp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAppUsersRepository _appUsersRepository;

        public ProductController(IProductsRepository productsRepository, ICategoriesRepository categoriesRepository, IOrdersRepository ordersRepository, IAppUsersRepository appUsersRepository)
        {
            _productsRepository = Guard.Against.Null(productsRepository);
            _categoriesRepository = Guard.Against.Null(categoriesRepository);
            _ordersRepository = Guard.Against.Null(ordersRepository);
            _appUsersRepository = Guard.Against.Null(appUsersRepository);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            ViewData["products"] = await _productsRepository.GetAllProductsAsync();
            ViewData["categories"] = await _categoriesRepository.GetCategoriesAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult IndexAdmin()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Orders()
        {
            ViewData["orders"] = await _ordersRepository.GetAllOrdersAsync();
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Order()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _appUsersRepository.GetUserByIdAsync(id);

            var order = new Order()
            {
                Products = user!.Products!,
                UserId = user.Id
            };

            await _ordersRepository.AddOrderAsync(order).ContinueWith(async (task) =>
            {
                await _appUsersRepository.ClearCart(id);
            }) ;

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Cart()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _appUsersRepository.GetUserByIdAsync(id);
            ViewData["products"] = user!.Products;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> FilterByCategory(int categoryId)
        {
            ViewData["products"] = await _productsRepository.GetProductsByCategory(categoryId);
            ViewData["categories"] = await _categoriesRepository.GetCategoriesAsync();
            return View("Index");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddToCartProduct(int id)
        {
            var product = await _productsRepository.GetProductAsync(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _appUsersRepository.AddProductToCart(product, userId);

            return RedirectToAction("Index");
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
