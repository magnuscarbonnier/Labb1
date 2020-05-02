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
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public YourOrders(IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }
        
            
        public List<Order> Orders = new List<Order>();
        public void OnGet()
        {
            HttpContext.Session.CheckUserId(HttpContext, _userManager);
            var orderresponse = HttpContext.Session.Get<List<Order>>(Lib.SessionKeyOrderList);
        if(orderresponse!=null)
            {
                Orders = orderresponse;
            }
        }
    }
}
