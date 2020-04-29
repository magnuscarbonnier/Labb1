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
                Description = "Ewan",
                Name = "Tshirt",
                Price = 9.95M,
                ImgSrc = "https://i.pinimg.com/736x/7a/c8/a7/7ac8a76ab34814275c2f3326bc79d437.jpg"
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Description = "Yoda",
                Name = "Leggings",
                Price = 19.95M,
                ImgSrc = "https://cdn.shopify.com/s/files/1/1705/1793/products/product-image-186869758_753_800x.jpg?v=1513580964"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Anders Tegnell S",
                Name = "Tshirt",
                Price = 199.90M,
                ImgSrc = "https://img.tradera.net/images/327/349543327_9d67fb0e-f063-4fe8-99f7-df6e56aba590.jpg"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Anders Tegnell M",
                Name = "Tshirt",
                Price = 199.90M,
                ImgSrc = "https://img.tradera.net/images/327/349543327_9d67fb0e-f063-4fe8-99f7-df6e56aba590.jpg"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Anders Tegnell XXL",
                Name = "Tshirt",
                Price = 199.90M,
                ImgSrc = "https://img.tradera.net/images/327/349543327_9d67fb0e-f063-4fe8-99f7-df6e56aba590.jpg"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Obi Wan S",
                Name = "Tshirt",
                Price = 299.95M,
                ImgSrc = "https://www.dhresource.com/0x0/f2/albu/g10/M00/AF/A9/rBVaWV5Xj1SANZR_AAC3PMUet4w567.jpg"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Obi Wan M",
                Name = "Tshirt",
                Price = 299.95M,
                ImgSrc = "https://www.dhresource.com/0x0/f2/albu/g10/M00/AF/A9/rBVaWV5Xj1SANZR_AAC3PMUet4w567.jpg"
            },
            new Product()
        {
            Id = Guid.NewGuid(),
                Description = "Obi Wan L",
                Name = "Tshirt",
                Price = 248.95M,
                ImgSrc = "https://www.dhresource.com/0x0/f2/albu/g10/M00/AF/A9/rBVaWV5Xj1SANZR_AAC3PMUet4w567.jpg"
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
