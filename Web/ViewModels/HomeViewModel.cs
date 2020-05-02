using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Top3Products { get; set; }
    }
}
