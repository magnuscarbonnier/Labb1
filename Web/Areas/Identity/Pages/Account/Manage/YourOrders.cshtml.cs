using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Web.Services;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class YourOrders : PageModel
    {
        public List<Order> Orders = new List<Order>();
        public void OnGet()
        {
            var orderresponse = HttpContext.Session.Get<List<Order>>(Lib.SessionKeyOrderList);
        if(orderresponse!=null)
            {
                Orders = orderresponse;
            }
        }
    }
}
