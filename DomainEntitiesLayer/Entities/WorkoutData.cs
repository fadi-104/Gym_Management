using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Entities
{
    public class WorkoutData : BaseEntity
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int? ExercisId { get; set; }
        [Range(1,10)]
        public int SetNumber { get; set; }
        [MinLength(1)]
        public int RepsNumber { get; set; }
        public float Weight { get; set; }


        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(ExercisId))]
        public Exercise Exercise { get; set; }
    }
}
