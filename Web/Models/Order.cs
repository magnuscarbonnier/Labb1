using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Models
{
    public class Order
    {
        public Guid Id { get; set; }
 
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "AnvändarID")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Totalpris")]
        public decimal TotalPrice { get; set; }

        public List<OrderRow> OrderRows { get; set; }

        
        
        public Order()
        {
            OrderRows = new List<OrderRow>();
            Id = Guid.NewGuid();
        } 
    }

    public class OrderRow
    {
        public OrderRow()
        {

        }

        public static explicit operator OrderRow(CartProduct cartProduct)
        {
            var orderRow = new OrderRow()
            {
                Quantity = cartProduct.Quantity,
                Product = cartProduct.Product
            };
            return orderRow;
        }
        [Required]
        [Display(Name = "Produkt")]
        public Product Product { get; set; }
       
        [Display(Name = "Antal")]
        public int Quantity { get; set; }
    }
}