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

        public int Power { get; set; }
        public int Duaration { get; set; }
        public int WorkoutId { get; set; }
        public Guid CustomerId { get; set; }
        public Workout Workout { get; set; }
       
    }
}
