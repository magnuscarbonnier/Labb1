using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ICartService _cartService;

        public CartController(IProductService productService, UserManager<ApplicationUser> userManager, ICartService cartService)
        {
            _productService = productService;
            _userManager = userManager;
            _cartService = cartService;
        }

        [HttpGet]
        public JsonResult GetCartAmount()
        {
            var userId = _userManager.GetUserId(User);
            var cart = _cartService.GetCart(userId, HttpContext.Session);
            var totalitems = cart.CartItems.Sum(x => x.Quantity);
            return new JsonResult(totalitems);
        }
        [HttpGet]
        public IActionResult AddToCart(Guid Id)
        {
            var product = _productService.GetById(Id);
            var userId = _userManager.GetUserId(User);
            var message = _cartService.AddItemToCart(userId, product, HttpContext.Session);

            var cart = _cartService.GetCart(userId, HttpContext.Session);
            var totalitems = cart.CartItems.Sum(x => x.Quantity);


            return Ok(totalitems);
        }
    }
}