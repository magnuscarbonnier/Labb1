using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
            string currentCart = HttpContext.Session.GetString(Lib.SessionKeyCart);
            string cartContent = "";

            if (currentCart != null)
            {
                cartContent = currentCart;
                cartContent += "," + Id;
            }
            else
            {
                cartContent += Id;
            }

            HttpContext.Session.SetString(Lib.SessionKeyCart, cartContent);

            return RedirectToAction("index");
        }
    }
}
 