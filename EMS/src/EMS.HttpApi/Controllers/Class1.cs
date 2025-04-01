using EMS.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/cart")]
    public class CartController : EMSController
    {
        private readonly ICartAppService _cartAppService;

        public CartController(ICartAppService cartAppService)
        {
            _cartAppService = cartAppService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartDto input)
        {
            await _cartAppService.AddToCartAsync(input.ProductId, input.Quantity);
            return Ok();
        }

        [HttpGet("items")]
        public async Task<List<CartItemDto>> GetCartItemsAsync()
        {
            return await _cartAppService.GetCartItemsAsync();
        }
    }

    [Microsoft.AspNetCore.Components.Route("api/order")]
    public class OrderController : EMSController
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] bool isCod)
        {
            var orderId = await _orderAppService.CreateOrderAsync(isCod);
            return Ok(new { OrderId = orderId });
        }
    }

}
