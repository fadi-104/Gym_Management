using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Entities
{
    public class TraineeData : BaseEntity
    {
        public int TraineeId { get; set; }
        [Range(30,250)]
        public float Weight { get; set; }
        [Range(100,250)]
        public int Height { get; set; }
        [Range(30, 250)]
        public float WeightGoal { get; set; }
        public string Goal { get; set; }

        [ForeignKey(nameof(TraineeId))]
        public AppUser User { get; set; }

    }
}
