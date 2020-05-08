using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IProductService productService, UserManager<ApplicationUser> userManager, IOrderService orderService, ICartService cartService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
            _cartService = cartService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var order = _orderService.GetOrder(userId, HttpContext.Session);
            var cart = _cartService.GetCart(userId, HttpContext.Session);
            var message = Lib.OrderNotAdded;
            
            if (order != null || cart != null)
            {
                order.OrderItems = cart.CartItems;
                message = _orderService.AddOrder(userId, order, HttpContext.Session);               
            }
            else
            {
                TempData["Error"] = message;
                return RedirectToAction("Index", "Product");
            }
            return View(order);
        }

        [Authorize]
        public IActionResult Details()
        {
            var userId = _userManager.GetUserId(User);
            var order = _orderService.GetOrder(userId, HttpContext.Session);

            if (order == null)
            {
                TempData["Error"] = Lib.OrderNotGet;
                return RedirectToAction("Index", "Product");
            }

            return View(order);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Index(Order order)
        {
            if (!ModelState.IsValid)
                return View(order);
            var userId = _userManager.GetUserId(User);
            var message = Lib.OrderNotAdded;
            var cart = _cartService.GetCart(userId, HttpContext.Session);
            if (order != null && order.UserId == userId && cart.CartItems.Count>0)
            {
                order.OrderItems = cart.CartItems;
                message = _orderService.PlaceOrder(userId, order, HttpContext.Session);
                //empty cart
                HttpContext.Session.Remove(Lib.SessionKeyCart);
                return RedirectToAction("Details", "Order");
            }

            if (message == Lib.OrderNotAdded)
            {
                TempData["Error"] = message;
            }
            
                return RedirectToAction("Index", "Cart");            
        }

        [Authorize]
        public IActionResult Remove(Guid Id)
        {
            var userid = _userManager.GetUserId(User);
            var product = _productService.GetById(Id);
            var message = _cartService.RemoveItem(userid, product, HttpContext.Session);
            if (message == Lib.CartNotUpdated)
                TempData["Error"] = message;
             
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
             
            return RedirectToAction("index");
        }
    }
}