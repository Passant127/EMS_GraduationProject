using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EMS.IServices
{
    public interface IProductAppService:
     ICrudAppService<
       ProductDto,
       int,
       PagedAndSortedResultRequestDto,
       CreatedUpdatedProductDto>
    {
    }

public class ProductDto:EntityDto<int>
    {
       
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CreatedUpdatedProductDto
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }

}
