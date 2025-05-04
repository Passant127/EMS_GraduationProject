using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Entities
{
    public class FitnessInfo : FullAuditedAggregateRoot<int>
    {
        public int OnOrOff { get; set; }
        public int Mode { get; set; }
        public int Time { get; set; }
        public int Power { get; set; }
        public string? CustomerId { get; set; }

        public int? WorkoutId { get; set; } // Now optional
        public Workout Workout { get; set; }

    }
}
