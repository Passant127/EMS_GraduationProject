using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.DTO
{
    public class WorkoutDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }

    }

    public class CreateUpdateWorkoutDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string VideoUrl { get; set; }
    }
}
