using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.DTO
{
    public class FitnessInfoDto : FullAuditedEntityDto<int>
    {

        public int Power { get; set; }
        public int Duaration { get; set; }
        public string WorkoutName { get; set; }
        public string WorkoutDescription { get; set; }
        public string WorkoutVideoUrl { get; set; }
        public Guid? CustomerId { get; set; }
    }

   

    public class CreateUpdateFitnessInfoDto
    {

        public int Power { get; set; }
        public int Duaration { get; set; }
        public int WorkoutId { get; set; }
        public Guid? CustomerId { get; set; }

    }
}
