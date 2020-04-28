using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class CartViewModel
    {
        public List<Item> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
