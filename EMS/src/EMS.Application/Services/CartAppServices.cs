using EMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using EMS.IServices;
using Volo.Abp.Users;

namespace EMS.Services
{
    public class CartAppService : ApplicationService, ICartAppService
    {
        private readonly IRepository<CartItem, Guid> _cartRepository;
        private readonly IRepository<Product, int> _productRepository;

        public CartAppService(IRepository<CartItem, Guid> cartRepository, IRepository<Product, int> productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task AddToCartAsync(int productId, int quantity)
        {
            if (CurrentUser.Id == null)
            {
                throw new UserFriendlyException("You must be logged in to add items to the cart.");
            }

            var product = await _productRepository.GetAsync(productId);
            if (product.QuantityAvailable < quantity)
            {
                throw new UserFriendlyException("Not enough stock available.");
            }

            var customerId = CurrentUser.GetId();
            var existingItem = await _cartRepository.FirstOrDefaultAsync(
                x => x.ProductId == productId && x.CustomerId == customerId
            );

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateAsync(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    
                    ProductId = productId,
                    Quantity = quantity,
                    CustomerId = customerId
                };
                await _cartRepository.InsertAsync(cartItem);
            }
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync()
        {
            if (CurrentUser.Id == null)
            {
                throw new UserFriendlyException("You must be logged in to view cart items.");
            }

            var customerId = CurrentUser.GetId();
            var cartItems = await _cartRepository.GetListAsync(x => x.CustomerId == customerId);

            // Get related product data
            var productIds = cartItems.Select(x => x.ProductId).Distinct().ToList();
            var products = await _productRepository.GetListAsync(x => productIds.Contains(x.Id));

            // Merge and return
            var result = cartItems.Select(cartItem =>
            {
                var product = products.FirstOrDefault(p => p.Id == cartItem.ProductId);
                var price = product?.Price ?? 0;

                return new CartItemDto
                {
                    ProductId = cartItem.ProductId,
                    ProductName = product?.Title ?? "Unknown",
                    Quantity = cartItem.Quantity,
                    Price = price,
                    TotalPrice = price * cartItem.Quantity,
                    ImageUrl = product?.ImageUrl
                };
            }).ToList();

            return result;
        }
        public async Task RemoveFromCartAsync(int productId)
        {
            if (CurrentUser.Id == null)
            {
                throw new UserFriendlyException("You must be logged in to remove items from the cart.");
            }

            var customerId = CurrentUser.GetId();

            var item = await _cartRepository.FirstOrDefaultAsync(x =>
                x.ProductId == productId && x.CustomerId == customerId);

            if (item == null)
            {
                throw new UserFriendlyException("Item not found in your cart.");
            }

            await _cartRepository.DeleteAsync(item);
        }

        public async Task UpdateCartItemAsync(int productId, int quantity)
        {
            if (CurrentUser.Id == null)
            {
                throw new UserFriendlyException("You must be logged in to update cart items.");
            }

            if (quantity <= 0)
            {
                throw new UserFriendlyException("Quantity must be greater than zero.");
            }

            var customerId = CurrentUser.GetId();

            var cartItem = await _cartRepository.FirstOrDefaultAsync(x =>
                x.ProductId == productId && x.CustomerId == customerId);

            if (cartItem == null)
            {
                throw new UserFriendlyException("Cart item not found.");
            }

            var product = await _productRepository.GetAsync(productId);
            if (product.QuantityAvailable < quantity)
            {
                throw new UserFriendlyException("Not enough stock available.");
            }

            cartItem.Quantity = quantity;
            await _cartRepository.UpdateAsync(cartItem);
        }


    }
}
