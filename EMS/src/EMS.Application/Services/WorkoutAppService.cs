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
    public class WorkoutAppService : CrudAppService<
     Workout, // The Entity
     WorkoutDto, // Used to show entities
     int, // Primary Key
     PagedAndSortedResultRequestDto, // Paging and sorting
     CreateUpdateWorkoutDto>, // Used to create/update
     IWorkoutAppService
    {
        public WorkoutAppService(IRepository<Workout, int> repository)
            : base(repository)
        {
        }
    }
}
