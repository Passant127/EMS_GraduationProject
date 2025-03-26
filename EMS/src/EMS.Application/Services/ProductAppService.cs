using EMS.Entities;
using EMS.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EMS.Services
{
    public class ProductAppService :
    CrudAppService<
         Product, 
         ProductDto, 
         int, 
         PagedAndSortedResultRequestDto, 
         CreatedUpdatedProductDto>, 
         IProductAppService
    {
        public ProductAppService(IRepository<Product, int> repository)
            : base(repository)
        {
        }
    }
}

