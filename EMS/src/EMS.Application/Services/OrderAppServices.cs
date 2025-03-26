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
            var cartItems = await _cartRepository.GetListAsync();

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

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    Price = product.Price
                });
            }

            var order = new Order
            {
                UserId = CurrentUser.Id.GetValueOrDefault(), // Assuming you have access to the current user
                TotalAmount = totalAmount,
                Status = OrderStatus.Placed,
                IsCOD = isCod,
                OrderItems = orderItems
            };

            await _orderRepository.InsertAsync(order);
            return order.Id;
        }

       

        public async Task<OrderSummaryDto> GetOrderSummaryAsync()
        {
            var cartItems = await _cartRepository.GetListAsync();
            var products = await _productRepository.GetListAsync();

            decimal subtotal = 0;

            foreach (var item in cartItems)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    subtotal += product.Price * item.Quantity;
                }
            }

            decimal shipping = 50.0M; // أو dynamic حسب نوع الشحن
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
