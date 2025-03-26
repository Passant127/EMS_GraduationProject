using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMS.IServices
{


    public class OrderSummaryDto
    {
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
    }

    public interface IOrderAppService
    {
        Task<Guid> CreateOrderAsync(bool isCod);
        Task<OrderSummaryDto> GetOrderSummaryAsync();
    }

}
