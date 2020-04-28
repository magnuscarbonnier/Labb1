using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            CartViewModel vm = new CartViewModel();
            var currentCart = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);
            vm.CartItems = currentCart;
            vm.TotalPrice = vm.CartItems.Sum(x => x.Product.Price * x.Quantity);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            //OrderViewModel vm = new OrderViewModel();
            var cart = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);
            var total = HttpContext.Session.Get<decimal>(Lib.SessionKeyTotalPrice);
            var user = await _userManager.GetUserAsync(User);
            Order order = new Order 
            { 
                UserId=user.Id, 
                OrderItems= cart, 
                TotalPrice=total, 
                Email=user.Email, 
                FirstName=user.FirstName, 
                LastName=user.LastName, 
                Address=user.Address,
                ZipCode=user.ZipCode,
                City=user.City,
                Phone=user.PhoneNumber,
                OrderDate=DateTime.Now, 
                Status=Lib.Status.Beställd
            };
            //vm.User = user;
            //vm.Order = order;
            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);
            return RedirectToAction("Index", "Order");
        }

    }
}