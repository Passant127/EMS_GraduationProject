using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Entities;

namespace EMS.Entities
{
    public class Order : AuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public bool IsCOD { get; set; }
    }

    public enum OrderStatus
    {
        Placed,
        Shipped,
        Delivered
    }

    public class OrderItem : Entity<Guid>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
