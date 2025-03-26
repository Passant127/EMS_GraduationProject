using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;

namespace EMS.Entities
{
    public class Product : FullAuditedAggregateRoot<int>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } 
    }
 

}
