using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ICartService _cartService;

        public ProductController(IProductService productService, UserManager<ApplicationUser> userManager, ICartService cartService)
        {
            _productService = productService;
            _userManager = userManager;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            ProductViewModel vm = new ProductViewModel();
            var products = _productService.GetAll();
            vm.Products = products;
            return View(vm);
        }

        public IActionResult AddToCart(Guid Id)
        {
            var product = _productService.GetById(Id);
            var userId = _userManager.GetUserId(User);
            var message = _cartService.AddItemToCart(userId, product, HttpContext.Session);

            if (message == Lib.CartNotUpdated)
                TempData["Error"] = message;

            return RedirectToAction("index", "Cart");
        }
    }
}
 