using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMS.DTO
{
    public class DeviceCommandDto
    {
        public int OnOrOff { get; set; }
        public int Mode { get; set; }
        public int Time { get; set; }
        public int Power { get; set; }
        public string? CustomerId { get; set; }
    }

    public class DeviceCommandRawDto
    {
        public string RawCommand { get; set; }
    }


    public class FitnessInfoDto : EntityDto<int>
    {
        public int OnOrOff { get; set; }
        public int Mode { get; set; }
        public int Time { get; set; }
        public  int Power { get; set; }
        public string? CustomerId { get; set; }
        public WorkoutDto Workout { get; set; }
    }

    public class CreateUpdateFitnessInfoDto
    {
        public int OnOrOff { get; set; }
        public int Mode { get; set; }
        public int Time { get; set; }
        public int Power { get; set; }
        public string? CustomerId { get; set; }
        public int? WorkoutId { get; set; }
    }
}
