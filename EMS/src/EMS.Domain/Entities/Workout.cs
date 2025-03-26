using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMS.Entities
{
    public class Workout : FullAuditedAggregateRoot<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Category { get; set; } // Category for the exercise (Biceps, Triceps, etc.)
    }

  
}
