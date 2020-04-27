using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class CartViewModel
    {
        public List<CartProduct> CartProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class CartProduct
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}
