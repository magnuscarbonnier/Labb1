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

        public ProductController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
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
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            if(sessionUser!=userid)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Set<string>(Lib.SessionKeyUserId,userid);
            }

            var product = _productService.GetById(Id);
            if(product == null)
            {
                return RedirectToAction("index");
            }
            
            var items=HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);

            if (items != null)
            {
                if (items.Any(s => s.Product.Id == Id))
                {
                        int itemIndex = items.FindIndex(x => x.Product.Id==Id);
                        items[itemIndex].Quantity += 1;
                }
                else
                {
                    items.Add(new Item() { Quantity = 1, Product = product });
                }  
            }
            else
            {
                items = new List<Item>() { new Item { Product = product, Quantity = 1 } };
            }
            var totalPrice = items.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, totalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, items);

            TempData["Success"] = $"{product.Name} tillagd i varukorgen.";
            return RedirectToAction("index");
        }
    }
}
 