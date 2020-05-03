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
        //session
        public const string SessionKeyCart = "_ShoppingCart";
        public const string SessionKeyTotalPrice = "_ShoppingTotalPrice";
        public const string SessionKeyOrder = "_ShoppingOrder";
        public const string SessionKeyOrderList = "_OrderList";
        public const string SessionKeyUserId = "_UserId";

        //status
        public enum Status { Beställd = 0, Bekräftad = 1, Packas = 2, Skickad = 3, Avbeställd = 4 }

        //cart
        public const string CartNotUpdated = "Kunde inte ändra i varukorgen.";
        public const string CartAddAlreadyInCart = "Du hade redan varan i varukorgen. Uppdaterade antalet med 1.";
        public const string CartAdd = "Varan lades till i varukorgen";
        public const string CartRemove = "Varan togs bort från varukorgen";
        public const string CartRemoveOne = "Minskade antal med 1 från varan.";

        //order
        public const string OrderAdd = "Order lades till.";
        public const string OrderNotAdded = "Order kunde inte läggas till.";
        public const string OrderNotGet = "Order kunde inte hämtas";
    }
}

