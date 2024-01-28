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
        public async Task<IActionResult> IndexAdmin()
        {
            ViewData["products"] = await _productsRepository.GetAllProductsAsync();
            ViewData["categories"] = await _categoriesRepository.GetCategoriesAsync();
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Orders()
        {
            ViewData["orders"] = await _ordersRepository.GetAllOrdersAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> OrdersAdmin()
        {
            ViewData["orders"] = await _ordersRepository.GetAllOrdersAdminAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("setorderstatus/{id}/{status}")]
        public async Task<IActionResult> SetOrderStatus(int id,bool status)
        {
            await _ordersRepository.SetOrderStatusAsync(id, status);
            return RedirectToAction("OrdersAdmin");
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Order()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _appUsersRepository.GetUserByIdAsync(id);

            var orderedProducts = user!.Products!.ToList();

            var order = new Order()
            {
                Products = orderedProducts,
                UserId = user.Id,
                IsAccepted = false,
            };

            await _ordersRepository.AddOrderAsync(order);

            await _appUsersRepository.ClearCart(id);

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
        public async Task<IActionResult> AddProduct()
        {
            ViewData["categories"] = await _categoriesRepository.GetCategoriesAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel addProductViewModel)
        {
            await _productsRepository.AddProductAsync(addProductViewModel);
            return RedirectToAction("IndexAdmin");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productsRepository.RemoveProductAsync(id);
            return RedirectToAction("IndexAdmin");
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewData["product"] = await _productsRepository.GetProductAsync(id);
            ViewData["categories"] = await _categoriesRepository.GetCategoriesAsync();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int id, ProductViewModel editProductViewModel)
        {
            await _productsRepository.UpdateProductAsync(id, editProductViewModel);
            return RedirectToAction("IndexAdmin");
        }

    }
}
