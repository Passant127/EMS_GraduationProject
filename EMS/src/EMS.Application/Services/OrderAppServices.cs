using EMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Volo.Abp;
using EMS.IServices;

namespace EMS.Services
{
    public class OrderAppService : ApplicationService, IOrderAppService
    {
        private readonly IRepository<CartItem, Guid> _cartRepository;
        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, int> _productRepository;

        public OrderAppService(IRepository<CartItem, Guid> cartRepository, IRepository<Order, Guid> orderRepository, IRepository<Product, int> productRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }



        public async Task<Guid> CreateOrderAsync(bool isCod)
        {
            if (CurrentUser.Id == null)
            {
                throw new UserFriendlyException("You must be logged in to place an order.");
            }

            var customerId = CurrentUser.GetId();
            var cartItems = await _cartRepository.GetListAsync(x => x.CustomerId == customerId);

            if (cartItems.Count == 0)
            {
                throw new UserFriendlyException("Your cart is empty.");
            }

            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                var product = await _productRepository.GetAsync(cartItem.ProductId);

                totalAmount += product.Price * cartItem.Quantity;

                // 🛠️ DO NOT set Id — EF will generate it
                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    Price = product.Price
                });
            }

            var order = new Order
            {

                UserId = customerId,
                TotalAmount = totalAmount,
                Status = OrderStatus.Placed,
                IsCOD = isCod,
                OrderItems = orderItems
            };

            await _orderRepository.InsertAsync(order, autoSave: true);

            return order.Id;
        }




        public async Task<OrderSummaryDto> GetOrderSummaryAsync()
        {
            if (CurrentUser.Id == null)
            {
                throw new UserFriendlyException("You must be logged in to view the order summary.");
            }

            var customerId = CurrentUser.GetId();

            // ✅ Get only cart items belonging to the current customer
            var cartItems = await _cartRepository.GetListAsync(x => x.CustomerId == customerId);

            if (!cartItems.Any())
            {
                throw new UserFriendlyException("Your cart is empty.");
            }

            var productIds = cartItems.Select(x => x.ProductId).Distinct().ToList();
            var products = await _productRepository.GetListAsync(p => productIds.Contains(p.Id));

            decimal subtotal = 0;

            foreach (var item in cartItems)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    subtotal += product.Price * item.Quantity;
                }
            }

            decimal shipping = 50.0M; // Can be dynamic based on rules
            decimal total = subtotal + shipping;

            return new OrderSummaryDto
            {
                Subtotal = subtotal,
                Shipping = shipping,
                Total = total
            };
        }

    }
}
