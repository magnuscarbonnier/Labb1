using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Item
    {
        public Product Product { get; set; }
        
        [Display(Name = "Antal")]
        public int Quantity { get; set; }

        public Item()
        {
            Product = new Product();
        }
    }
}
