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
        public IActionResult Index()
        {
            var order=HttpContext.Session.Get<Order>(Lib.SessionKeyOrder);
            return View(order);
        }
    }
}