
using EMS.DTO;
using EMS.Entities;
using EMS.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace EMS.Services
{
    public class FitnessInfoAppService : CrudAppService<
          FitnessInfo, FitnessInfoDto, int,
          PagedAndSortedResultRequestDto, CreateUpdateFitnessInfoDto>, IFitnessInfoAppService
    {
        private readonly ICurrentUser _currentUser;

        public FitnessInfoAppService(IRepository<FitnessInfo, int> repository, ICurrentUser currentUser)
            : base(repository)
        {
            _currentUser = currentUser;
        }

        public override async Task<PagedResultDto<FitnessInfoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var customerId = _currentUser.Id;
            var query = Repository.GetQueryableAsync().Result
                .Include(s => s.Workout)
                .Where(f => f.CustomerId == customerId.ToString());

            if (!string.IsNullOrEmpty(input.Sorting))
            {
                query = ApplySorting(query, input.Sorting);
            }

            var totalCount = await query.CountAsync();
            var items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            var result = ObjectMapper.Map<List<FitnessInfo>, List<FitnessInfoDto>>(items);
            return new PagedResultDto<FitnessInfoDto>(totalCount, result);
        }

        private IQueryable<FitnessInfo> ApplySorting(IQueryable<FitnessInfo> query, string sorting)
        {
            return query.OrderBy(sorting);
        }

        public override async Task<FitnessInfoDto> CreateAsync(CreateUpdateFitnessInfoDto input)
        {
            input.CustomerId = _currentUser.Id.ToString();
            var fitnessInfo = ObjectMapper.Map<CreateUpdateFitnessInfoDto, FitnessInfo>(input);
            var saved = await Repository.InsertAsync(fitnessInfo);
            return ObjectMapper.Map<FitnessInfo, FitnessInfoDto>(saved);
        }

        // ABP-style: POST: Create command string
        public async Task<DeviceCommandRawDto> GenerateCommandStringAsync(DeviceCommandDto input)
        {
            var parts = new List<string>
            {
                input.OnOrOff.ToString(),
                input.Mode.ToString(),
                input.Time.ToString(),
                input.Power.ToString()
            };

            var result = string.Join(";", parts);
            return new DeviceCommandRawDto { RawCommand = result };
        }

        // ABP-style: PUT: Update command by ID
        public async Task<DeviceCommandRawDto> UpdateCommandStringAsync(int id, DeviceCommandDto input)
        {
            var entity = await Repository.GetAsync(id);
            entity.OnOrOff = input.OnOrOff;
            entity.Mode = input.Mode;
            entity.Time = input.Time;
            entity.Power = input.Power;
            entity.CustomerId = input.CustomerId;

            await Repository.UpdateAsync(entity);

            var raw = string.Join(";", new[]
            {
                input.OnOrOff.ToString(),
                input.Mode.ToString(),
                input.Time.ToString(),
                input.Power.ToString(),
                input.CustomerId?.Trim()
            }.Where(x => !string.IsNullOrEmpty(x)));

            return new DeviceCommandRawDto { RawCommand = raw };
        }

        // ABP-style: GET: Get structured payload by ID
        public async Task<DeviceCommandDto> GetCommandAsPayloadAsync(int id)
        {
            var entity = await Repository.GetAsync(id);
            return new DeviceCommandDto
            {
                OnOrOff = entity.OnOrOff,
                Mode = entity.Mode,
                Time = entity.Time,
                Power = entity.Power,
                CustomerId = entity.CustomerId
            };
        }


        public async Task<DeviceCommandRawDto> GetCommandStringByIdAsync(int id)
        {
            var entity = await Repository.GetAsync(id);
            var parts = new List<string>
            {
                entity.OnOrOff.ToString(),
                entity.Mode.ToString(),
                entity.Time.ToString(),
                entity.Power.ToString()
            };


            var raw = string.Join(";", parts);
            return new DeviceCommandRawDto { RawCommand = raw };

        }
    }
}

