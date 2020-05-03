using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class CartService : ICartService
    {
        public Cart GetCart(string userId, ISession session)
        {
            var existingCart = session.Get<Cart>(Lib.SessionKeyCart);
            var cart = new Cart();

            if (existingCart != null && userId != null && userId == existingCart.CartUserId)
            {
                cart = existingCart;
            }

            else if (userId != null)
            {
                cart = new Cart() { CartUserId = userId, CartItems = new List<Item>() };
            }

            return cart;
        }

        public string AddItemToCart(string userId, Product product, ISession session)
        {
            var existingCart = session.Get<Cart>(Lib.SessionKeyCart);
            var cart = new Cart();
            var message = Lib.CartNotUpdated;

            if (userId != null && product != null)
            {
                if (existingCart != null && userId == existingCart.CartUserId)
                {
                    cart = existingCart;
                    if (cart.CartItems.Any(s => s.Product.Id == product.Id))
                    {
                        int itemIndex = cart.CartItems.FindIndex(x => x.Product.Id == product.Id);
                        cart.CartItems[itemIndex].Quantity++;
                        message = Lib.CartAddAlreadyInCart;
                    }
                    else
                    {
                        cart.CartItems.Add(new Item { Product = product, Quantity = 1 });
                        message = Lib.CartAdd;
                    }
                }
                else
                {
                    cart.CartUserId = userId;
                    cart.CartItems.Add(new Item { Product = product, Quantity = 1 });
                    message = Lib.CartAdd;
                }
                session.Set<Cart>(Lib.SessionKeyCart, cart);
            }
            return message;
        }

        public string AddOneItem(string userId, Product product, ISession session)
        {
            var existingCart = session.Get<Cart>(Lib.SessionKeyCart);
            var cart = new Cart();
            var message = Lib.CartNotUpdated;

            if (userId != null && product != null && userId == existingCart.CartUserId && existingCart.CartItems.Any(s => s.Product.Id == product.Id))
            {
                cart = existingCart;
                if (cart.CartItems.Any(s => s.Product.Id == product.Id))
                {
                    int itemIndex = cart.CartItems.FindIndex(x => x.Product.Id == product.Id);
                    cart.CartItems[itemIndex].Quantity++;
                    message = Lib.CartAddAlreadyInCart;

                    session.Set<Cart>(Lib.SessionKeyCart, cart);
                }
            }
            
            return message;
        }

        public string RemoveOneItem(string userId, Product product, ISession session)
        {
            var existingCart = session.Get<Cart>(Lib.SessionKeyCart);
            var cart = new Cart();
            var message = Lib.CartNotUpdated;

            if (userId != null && product != null && userId == existingCart.CartUserId && existingCart.CartItems.Any(s => s.Product.Id == product.Id))
            {
                cart = existingCart;
                if (cart.CartItems.Any(s => s.Product.Id == product.Id))
                {
                    int itemIndex = cart.CartItems.FindIndex(x => x.Product.Id == product.Id);

                    if (cart.CartItems.FirstOrDefault(c=>c.Product.Id==product.Id).Quantity==1)
                    {
                        message = Lib.CartRemove;
                        cart.CartItems.RemoveAt(itemIndex);
                    }
                    else
                    {
                        message = Lib.CartRemoveOne;
                        cart.CartItems[itemIndex].Quantity--;
                    }
                }

                session.Set<Cart>(Lib.SessionKeyCart, cart);
            }
            return message;
        }

        public string RemoveItem(string userId, Product product, ISession session)
        {
            var existingCart = session.Get<Cart>(Lib.SessionKeyCart);
            var cart = new Cart();
            var message = Lib.CartNotUpdated;

            if (userId != null && product != null && userId == existingCart.CartUserId && existingCart.CartItems.Any(s => s.Product.Id == product.Id))
            {
                cart = existingCart;
                if (cart.CartItems.Any(s => s.Product.Id == product.Id))
                {
                    int itemIndex = cart.CartItems.FindIndex(x => x.Product.Id == product.Id);
                    message = Lib.CartRemove;
                    cart.CartItems.RemoveAt(itemIndex);
                }
                session.Set<Cart>(Lib.SessionKeyCart, cart);
            }
            return message;
        }

        
    }
}
