using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Web.Services;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class YourOrders : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public YourOrders(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }
        
            
        public List<Order> Orders = new List<Order>();
        public void OnGet()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetOrders(userId, HttpContext.Session);
            
        if(orders!=null)
            {
                Orders = orders;
            }
        }
    }
}
