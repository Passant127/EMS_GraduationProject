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
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Application.Services;
    using Volo.Abp.Users;
    using System.Linq.Dynamic.Core;
    using AutoMapper.Internal.Mappers;
    using Microsoft.EntityFrameworkCore;

    namespace EMS
    {
        public class FitnessInfoAppService : CrudAppService<
            FitnessInfo, // The Entity
            FitnessInfoDto, // Used to show entities
            int, // Primary Key
            PagedAndSortedResultRequestDto, // Paging and sorting
            CreateUpdateFitnessInfoDto>, // Used to create/update
            IFitnessInfoAppService
        {
            private readonly ICurrentUser _currentUser;

            public FitnessInfoAppService(IRepository<FitnessInfo, int> repository, ICurrentUser currentUser)
                : base(repository)
            {
                _currentUser = currentUser;
            }

   

         public override async Task<PagedResultDto<FitnessInfoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            // Get CustomerId from the token (current user)
            var customerId = _currentUser.Id;

            // Get the queryable for FitnessInfo
            var query =  Repository.GetQueryableAsync().Result.Include(s=>s.Workout).Where(f => f.CustomerId == customerId);

  

            // Check if Sorting is provided
            if (!string.IsNullOrEmpty(input.Sorting))
            {
                // Apply dynamic sorting if Sorting string is provided
                query = ApplySorting(query, input.Sorting);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var result = ObjectMapper.Map<List<FitnessInfo>, List<FitnessInfoDto>>(items);
              

            return new PagedResultDto<FitnessInfoDto>(totalCount, result);
        }

        // Method to apply sorting dynamically based on the string
        private IQueryable<FitnessInfo> ApplySorting(IQueryable<FitnessInfo> query, string sorting)
        {
            // Apply sorting to the query based on the field
            // Assuming sorting string is like "PropertyName ASC" or "PropertyName DESC"
            return query.OrderBy(sorting);
        }

        // Override Create method to set CustomerId from token
        public override async Task<FitnessInfoDto> CreateAsync(CreateUpdateFitnessInfoDto input)
            {
                // Get CustomerId from the token (current user)
                var customerId = _currentUser.Id;

                // Set CustomerId on the DTO
                input.CustomerId = customerId;

                // Create the entity
                var fitnessInfo = ObjectMapper.Map<CreateUpdateFitnessInfoDto, FitnessInfo>(input);

                // Save to the database
                var savedFitnessInfo = await Repository.InsertAsync(fitnessInfo);

                return ObjectMapper.Map<FitnessInfo, FitnessInfoDto>(savedFitnessInfo);
            }
        }
    }


}
