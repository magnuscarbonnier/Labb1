using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class ProductViewModel
    {
        public string Namn { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
