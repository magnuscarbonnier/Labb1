using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class MockProductService : IProductService
    {
        public List<Product> products = new List<Product>()
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Kattoutfit",
                Name = "Hoodie",
                Price = 199.95M,
                ImgSrc = "https://cdn.shopify.com/s/files/1/0049/4423/2534/products/hosi-1_789bc507-8604-4b21-9edb-d8a56549730c_1000x1000.jpg?v=1571718188"
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Randig",
                Name = "Tröja",
                Price = 163M,
                ImgSrc = "https://cdn.shopify.com/s/files/1/0049/4423/2534/products/O1CN01DTynvH1SZ7cBdMaIj__1080692260_700x.jpg?v=1571718188"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Gubbmodell",
                Name = "Keps",
                Price = 59.90M,
                ImgSrc = "https://laughingsquid.com/wp-content/uploads/il_fullxfull.308715501.jpg"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Yoda",
                Name = "Mössa",
                Price = 119.90M,
                ImgSrc = "https://partycity6.scene7.com/is/image/PartyCity/_pdp_sq_?$_1000x1000_$&$product=PartyCity/630950"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Folie",
                Name = "Mössa",
                Price = 199.90M,
                ImgSrc = "https://cdn.shopify.com/s/files/1/1365/2497/products/tin-foil-hat-for-cats_2000x.jpg?v=1522252551"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Lejon",
                Name = "Mössa",
                Price = 99.95M,
                ImgSrc = "https://cdn11.bigcommerce.com/s-sabbaqv0i5/images/stencil/1000x1000/products/200/911/Funny-Cute-Pet-Cat-Costume-Lion-Mane-Wig-Cap-Hat-for-Cat-Dog-Halloween-Christmas-Clothes__86779.1565780913.jpg?c=2&imbypass=on"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Totoro",
                Name = "Maskeraddräkt",
                Price = 146M,
                ImgSrc = "https://cdn11.bigcommerce.com/s-sabbaqv0i5/images/stencil/1000x1000/products/979/5412/Warm-Soft-Fleece-Pet-Dog-Cat-Clothes-Cartoon-Puppy-Dog-Costumes-Autumn-Winter-Clothing-For-Small__02774.1570351119.jpg?c=2&imbypass=on"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Grå",
                Name = "Kattbädd",
                Price = 248.95M,
                ImgSrc = "https://cdn11.bigcommerce.com/s-sabbaqv0i5/images/stencil/1000x1000/products/134/527/Foldable-Cat-Bed-Self-Warming-for-Indoor-Cats-Dog-House-with-Removable-Mattress-Puppy-Cage-Lounger__48728.1565598870.jpg?c=2&imbypass=on"
            }

    };
        public MockProductService()
        {
            
        }
        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product GetById(Guid id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            return product;
        }
    }
}
