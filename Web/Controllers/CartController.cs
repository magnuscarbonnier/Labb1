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
            CheckUser();

            CartViewModel vm = new CartViewModel();
            var currentCart = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);
            if (currentCart == null)
                currentCart = new List<Item>();
            vm.CartItems = currentCart;
            vm.TotalPrice = vm.CartItems.Sum(x => x.Product.Price * x.Quantity);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            CheckUser();

            var cart = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);
            if(cart==null)
            {
                TempData["Error"] = "Lägg till varor i kundvagnen och försök igen...";
                return RedirectToAction("Index","Home");
            }
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

            HttpContext.Session.Set<Order>(Lib.SessionKeyOrder, order);
            return RedirectToAction("Index", "Order");
        }

        public IActionResult Remove(Guid Id)
        {
            CheckUser();

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("index");
            }

            var items = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);

            if (items != null && items.Any(s => s.Product.Id == Id))
            {

                int itemIndex = items.FindIndex(x => x.Product.Id == Id);
                items.RemoveAt(itemIndex);

            }

            var totalPrice = items.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, totalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, items);


            return RedirectToAction("index");
        }

        private void CheckUser()
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            if (sessionUser != userid)
            {
                HttpContext.Session.Clear();
                HttpContext.Session.Set<string>(Lib.SessionKeyUserId, userid);
            }
        }

        public IActionResult Increase(Guid Id)
        {
            CheckUser();

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("index");
            }

            var items = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);
            if (items != null)
            {
                if (items.Any(s => s.Product.Id == Id))
                {
                    int itemIndex = items.FindIndex(x => x.Product.Id == Id);
                    items[itemIndex].Quantity += 1;
                }
            }
           
            var totalPrice = items.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, totalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, items);

       
            return RedirectToAction("index");
        }
        public IActionResult Decrease(Guid Id)
        {
            CheckUser();

            var product = _productService.GetById(Id);
            if (product == null)
            {
                return RedirectToAction("index");
            }

            var items = HttpContext.Session.Get<List<Item>>(Lib.SessionKeyCart);

            if (items != null && items.Any(s => s.Product.Id == Id))
            {
                    int itemIndex = items.FindIndex(x => x.Product.Id == Id);
                    var itemQ = items[itemIndex].Quantity;
                    if(itemQ==1)
                        {
                        items.RemoveAt(itemIndex);
                  
                        }
                    else
                    {
                        items[itemIndex].Quantity--;
                    
                }
                
            }
            else
            {
                TempData["Error"] = "Fanns inte i varukorgen. Försök igen";
            }
            var totalPrice = items.Sum(x => x.Product.Price * x.Quantity);
            HttpContext.Session.Set<decimal>(Lib.SessionKeyTotalPrice, totalPrice);
            HttpContext.Session.Set<List<Item>>(Lib.SessionKeyCart, items);

            
            return RedirectToAction("index");
        }
    }
}