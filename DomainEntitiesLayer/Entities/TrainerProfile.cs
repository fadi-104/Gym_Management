using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Entities
{
    public class TrainerProfile : BaseEntity
    {
        public int TrainerId { get; set; }
        public string? Description { get; set; }
        public string? Experience { get; set; }
        public string? Championship { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public AppUser User { get; set; }
    }
}
