using AutoMapper.Internal.Mappers;
using EMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using EMS.IServices;

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
            var product = await _productRepository.GetAsync(productId);
            if (product.QuantityAvailable < quantity)
            {
                throw new UserFriendlyException("Not enough stock available.");
            }

            var cartItem = await _cartRepository.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                await _cartRepository.InsertAsync(cartItem);
            }
        }

        public async Task<List<CartItemDto>> GetCartItemsAsync()
        {
            var cartItems = await _cartRepository.GetListAsync();
            return ObjectMapper.Map<List<CartItem>, List<CartItemDto>>(cartItems);
        }
    }


}
