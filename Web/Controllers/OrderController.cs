using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        
        {
            var sessionUser = HttpContext.Session.Get<string>(Lib.SessionKeyUserId);
            var userid = _userManager.GetUserId(User);
            var order = HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);

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
    }
}