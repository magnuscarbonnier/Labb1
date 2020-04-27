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

            public IActionResult AddToCart(ProductViewModel vm)
            {
                //HttpContext.Session.SetString("Name", vm.Namn);
                return RedirectToAction("Index");
            }
        }
    }
    //public IActionResult Index()
//        {
//            vm.Namn =  HttpContext.Session.GetString("Name");
//                return View(vm);
//}
            
            
//            return View(vm);
//        }
