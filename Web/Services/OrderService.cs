using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public class OrderService : IOrderService
    {
        public string PlaceOrder(string userId, Order order, ISession session)
        {
            var existingOrders = session.Get<List<Order>>(Lib.SessionKeyOrderList);
            var orders = new List<Order>();
            var message = Lib.OrderNotAdded;

            if (userId != null && order != null && order.UserId == userId)
            {
                if(existingOrders != null && existingOrders.Any(c=>c.UserId==userId))
                {
                    orders = existingOrders;
                }
                message = Lib.OrderAdd;
                order.OrderDate = DateTime.Now;
                order.Status = Lib.Status.Beställd;
                orders.Add(order);
                session.Set<List<Order>>(Lib.SessionKeyOrderList, orders);
                session.Set<Order>(Lib.SessionKeyOrder, order);
                session.Remove(Lib.SessionKeyCart);
            }
            return message;
        }

        public Order GetOrder(string userId, ISession session)
        {
            var existingOrder= session.Get<Order>(Lib.SessionKeyOrder);
            var order = new Order();
            if (existingOrder != null && userId != null && existingOrder.UserId == userId)
            {
                order = existingOrder;
            }
            return order;
        }

        public List<Order> GetOrders(string userId, ISession session)
        {
            var existingOrders = session.Get<List<Order>>(Lib.SessionKeyOrderList);
            var orders = new List<Order>();

            if (existingOrders != null && userId != null && existingOrders.Any(c=>c.UserId==userId))
            {
                orders = existingOrders;
            }

            return orders;
        }

        public string AddOrder(string userId, Order order, ISession session)
        {
            var message = Lib.OrderNotAdded;
            if (order != null && order.UserId == userId && order.OrderItems!= null)
            {
                session.Set<Order>(Lib.SessionKeyOrder, order);
                message = Lib.OrderAdd;
            }
            return message;
        }
    }
}
