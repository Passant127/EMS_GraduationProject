using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMS.IServices
{
    public interface ICartAppService
    {
        Task AddToCartAsync(int productId, int quantity);
        Task<List<CartItemDto>> GetCartItemsAsync();
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }


}
