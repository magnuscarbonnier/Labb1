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
        [Authorize]
        public IActionResult Index()
        {
            //HttpContext.Session.CheckUserId(HttpContext, _userManager);
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            if (order == null)
            {
                TempData["Error"] = "Finns ingen aktiv order...";
                return RedirectToAction("Index", "Home");
            }
            else if(!order.OrderItems.CartItems.Any())
            {
                TempData["Error"]="Du har inga varor i din order. Lägg till varor och försök igen";
                return RedirectToAction("Index", "Product");
            }
            
            

            return View(order);
        }
        [Authorize]
        public IActionResult Details()
        {
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            
                return View(order);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Index(Order order)
        {
            if (!ModelState.IsValid)
                return View(order);
            HttpContext.Session.CheckUserId(HttpContext, _userManager);
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var cart = HttpContext.Session.Get<Cart>(Lib.SessionKeyCart);
            
            if (order.UserId == sessionUser)
            {
                var orderList = HttpContext.Session.Get<List<Order>>(Lib.SessionKeyOrderList);
                if (orderList == null)
                {
                    orderList = new List<Order>();
                }
                order.OrderItems = cart;
                orderList.Add(order);
                //Spara order till databasen istället
                HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);
                //Töm kundvagn
                HttpContext.Session.Remove(Lib.SessionKeyCart);
                //Spara till lista av ordrar
                HttpContext.Session.Set<List<Order>>(Lib.SessionKeyOrderList, orderList);
                return RedirectToAction("Details", "Order");
            }
            TempData["Error"] = "Finns ingen aktiv order...";
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Remove(Guid Id)
        {
            HttpContext.Session.CheckUserId(HttpContext, _userManager);
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
           
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
           

            var product = _productService.GetById(Id);
            if (product == null)
            {
                TempData["Error"] = "Finns ingen produkt med id "+Id;
                return RedirectToAction("Index", "Order");
            }

            if (order != null && order.OrderItems.CartItems.Any(s => s.Product.Id == Id))
            {

                int itemIndex = order.OrderItems.CartItems.FindIndex(x => x.Product.Id == Id);
                order.OrderItems.CartItems.RemoveAt(itemIndex);

            }

            order.TotalPrice = order.OrderItems.Total();
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, order.TotalPrice);
            HttpContext.Session.Set<Cart>(Lib.SessionKeyCart, order.OrderItems);
            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);


            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Increase(Guid Id)
        {
            HttpContext.Session.CheckUserId(HttpContext, _userManager);
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
           

            var product = _productService.GetById(Id);
            if (product == null)
            {
                TempData["Error"] = "Finns ingen produkt med id " + Id;
                return RedirectToAction("Index", "Order");
            }

            
            if (order.OrderItems != null)
            {
                if (order.OrderItems.CartItems.Any(s => s.Product.Id == Id))
                {
                    int itemIndex = order.OrderItems.CartItems.FindIndex(x => x.Product.Id == Id);
                    order.OrderItems.CartItems[itemIndex].Quantity += 1;
                }
            }

            order.TotalPrice = order.OrderItems.Total();
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, order.TotalPrice);
            HttpContext.Session.Set<Cart>(Lib.SessionKeyCart, order.OrderItems);

            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);


            return RedirectToAction("Index", "Order");
        }
        [Authorize]
        public IActionResult Decrease(Guid Id)
        {
            HttpContext.Session.CheckUserId(HttpContext, _userManager);
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            

            var product = _productService.GetById(Id);
            if (product == null)
            {
                TempData["Error"] = "Finns ingen produkt med id " + Id;
                return RedirectToAction("Index", "Order");
            }

            

            if (order.OrderItems != null && order.OrderItems.CartItems.Any(s => s.Product.Id == Id))
            {
                int itemIndex = order.OrderItems.CartItems.FindIndex(x => x.Product.Id == Id);
                var itemQ = order.OrderItems.CartItems[itemIndex].Quantity;
                if (itemQ == 1)
                {
                    order.OrderItems.CartItems.RemoveAt(itemIndex);

                }
                else
                {
                    order.OrderItems.CartItems[itemIndex].Quantity--;

                }

            }
            else
            {
                TempData["Error"] = "Fanns inte i varukorgen. Försök igen";
            }
            order.TotalPrice = order.OrderItems.Total();
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, order.TotalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, order.OrderItems.CartItems);
            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);


            return RedirectToAction("Index");
        }
    }
}