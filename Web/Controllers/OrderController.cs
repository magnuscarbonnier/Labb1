using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);

            if(!order.OrderItems.Any())
            {
                TempData["Error"]="Du har inga varor i din order. Lägg till varor och försök igen";
                return RedirectToAction("Index", "Product");
            }
            if (sessionUser != userid && userid != null)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Set<string>(Lib.SessionKeyUserId, userid);
                TempData["Error"] = "Lägg till varor i kundvagnen och försök igen...";
                return RedirectToAction("Index", "Home");
            }
            else if (order == null)
            {
                TempData["Error"] = "Finns ingen aktiv order...";
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }
        public IActionResult Details(Order order)
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            
            if (sessionUser==order.UserId)
            {
                return View(order);
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult SaveOrder(Order order)
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var cart = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);
            
            if (order.UserId == sessionUser)
            {
                order.OrderItems = cart;
                //Spara order till databasen istället
                HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);
                //Töm kundvagn
                HttpContext.Session.Remove(Lib.SessionKeyCart);
                //Töm order
                HttpContext.Session.Remove(Lib.SessionKeyOrder);
                return RedirectToAction("Details", "Order", order);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Remove(Guid Id)
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            if (sessionUser != userid)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Set<string>(Lib.SessionKeyUserId, userid);
            }

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("Index", "Order");
            }


            if (order != null && order.OrderItems.Any(s => s.Product.Id == Id))
            {

                int itemIndex = order.OrderItems.FindIndex(x => x.Product.Id == Id);
                order.OrderItems.RemoveAt(itemIndex);

            }

            order.TotalPrice = order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, order.TotalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, order.OrderItems);
            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);


            return RedirectToAction("Index");
        }

        public IActionResult Increase(Guid Id)
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            if (sessionUser != userid)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Set<string>(Lib.SessionKeyUserId, userid);
            }

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("Index", "Order");
            }

            
            if (order.OrderItems != null)
            {
                if (order.OrderItems.Any(s => s.Product.Id == Id))
                {
                    int itemIndex = order.OrderItems.FindIndex(x => x.Product.Id == Id);
                    order.OrderItems[itemIndex].Quantity += 1;
                }
            }

            order.TotalPrice = order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, order.TotalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, order.OrderItems);

            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);


            return RedirectToAction("Index", "Order");
        }
        public IActionResult Decrease(Guid Id)
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            if (sessionUser != userid)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Set<string>(Lib.SessionKeyUserId, userid);
            }

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("Index", "Order");
            }

            

            if (order.OrderItems != null && order.OrderItems.Any(s => s.Product.Id == Id))
            {
                int itemIndex = order.OrderItems.FindIndex(x => x.Product.Id == Id);
                var itemQ = order.OrderItems[itemIndex].Quantity;
                if (itemQ == 1)
                {
                    order.OrderItems.RemoveAt(itemIndex);

                }
                else
                {
                    order.OrderItems[itemIndex].Quantity--;

                }

            }
            else
            {
                TempData["Error"] = "Fanns inte i varukorgen. Försök igen";
            }
            order.TotalPrice = order.OrderItems.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, order.TotalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, order.OrderItems);
            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);


            return RedirectToAction("Index");
        }
    }
}