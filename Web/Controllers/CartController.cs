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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ICartService _cartService;

        public CartController(IProductService productService, UserManager<ApplicationUser> userManager, ICartService cartService)
        {
            _productService = productService;
            _userManager = userManager;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCart(userId, HttpContext.Session);
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCart(user.Id, HttpContext.Session);
            if(cart==null)
            {
                TempData["Error"] = "Lägg till varor i kundvagnen och försök igen...";
                return RedirectToAction("Index","Home");
            }
            var total = cart.Total();
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

            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);
            return RedirectToAction("Index", "Order");
        }
        [Authorize]
        public IActionResult Remove(Guid Id)
        {
            var userid = _userManager.GetUserId(User);
            var product = _productService.GetById(Id);
            var message = _cartService.RemoveItem(userid, product, HttpContext.Session);
            if (message == Lib.CartNotUpdated)
                TempData["Error"] = message;
            else
                TempData["Success"] = message;
            return RedirectToAction("index");
        }


        [Authorize]
        public IActionResult Increase(Guid Id)
        {
            var userid = _userManager.GetUserId(User);
            var product = _productService.GetById(Id);
            var message = _cartService.AddOneItem(userid, product, HttpContext.Session);
            if (message == Lib.CartNotUpdated)
                TempData["Error"] = message;
            else
                TempData["Success"] = message;
            return RedirectToAction("index");
        }
        [Authorize]
        public IActionResult Decrease(Guid Id)
        {
            var userid = _userManager.GetUserId(User);
            var product = _productService.GetById(Id);
            var message = _cartService.RemoveOneItem(userid, product, HttpContext.Session);
            if (message == Lib.CartNotUpdated)
                TempData["Error"] = message;
            else
                TempData["Success"] = message;
            return RedirectToAction("index");
        }
    }
}