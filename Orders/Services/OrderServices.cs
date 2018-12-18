﻿using System;
using System.Collections.Generic;
using System.Text;
using Orders.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderServices : IOrderService
    {
        private IList<Order> _orders;
        public OrderServices()
        {
            _orders = new List<Order>();
            _orders.Add(new Order("1000", "250 Conference brochures", DateTime.Now, 1, "FAEBD970-CBA5-4CED-8AD5-5CC0B8D4B7827"));
            _orders.Add(new Order("2000", "250 t-shirts", DateTime.Now.AddHours(1), 2, "F43A1F6D-4AE9-4A19-93D9-2018387D5378"));
            _orders.Add(new Order("3000", "500 Stickers", DateTime.Now.AddHours(2), 3, "2D542571-EF99-4786-AEB8-C997D82E57C7"));
            _orders.Add(new Order("4000", "10 Posters", DateTime.Now.AddHours(2), 4, "2D542571-EF99-4786-AEB5-C997D82E57C7"));

        }

        private Order GetById(string id)
        {
            var order = _orders.SingleOrDefault(o => Equals(o.Id, id));
            if (order == null)
            {
                throw new ArgumentException($"Order ID ${id} is invalid");
            }
            return order;
        }

        public Task<Order> CreateAsync(Order order)
        {
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<IEnumerable<Order>> GetOrderAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        public Task<Order> GetOrderByIdAsync(string id)
        {
            return Task.FromResult(_orders.Single(o => Equals(o.Id, id)));
        }

        public Task<Order> StartAsync(string orderId)
        {
            var order = GetById(orderId);
            order.Start();
            return Task.FromResult(order);
        }
    }

    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(string id);
        Task<IEnumerable<Order>> GetOrderAsync();
        Task<Order> CreateAsync(Order order);
        Task<Order> StartAsync(string orderId);
    }
}
