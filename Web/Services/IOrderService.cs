using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface IOrderService
    {
        List<Order> GetOrders(string userId, ISession session);
        string PlaceOrder(string userId, Order order, ISession session);
        Order GetOrder(string userId, ISession session);
        string AddOrder(string userId, Order order, ISession session);
    }
}
