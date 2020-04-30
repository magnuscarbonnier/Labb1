using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Services;

namespace Web
{
    public class Lib
    {
        public const string SessionKeyCart = "_ShoppingCart";
        public const string SessionKeyTotalPrice = "_ShoppingTotalPrice";
        public const string SessionKeyOrder = "_ShoppingOrder";
        public const string SessionKeyOrderList = "_OrderList";
        public const string SessionKeyUserId = "_UserId";
        public enum Status { Beställd = 0, Bekräftad = 1, Packas = 2, Skickad = 3, Avbeställd = 4 }
        
       
    }
}

