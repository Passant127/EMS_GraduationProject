using EMS.DTO;
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
    
    public class RecipeAppService : CrudAppService<
         Recipe, // The Entity
         RecipeDto, // Used to show entities
         int, // Primary Key
         PagedAndSortedResultRequestDto, // Paging and sorting
         CreateUpdateRecipeDto>, // Used to create/update
         IRecipeAppService
            {
                public RecipeAppService(IRepository<Recipe, int> repository)
                    : base(repository)
                {
                }
            }
}
