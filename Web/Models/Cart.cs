using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Cart
    {
        public string CartUserId { get; set; }
        public List<Item> CartItems { get; set; } = new List<Item>();
        
        public decimal Total()
        {
            return CartItems.Sum(x => x.Product.Price * x.Quantity);
        }
    }
}
