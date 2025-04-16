using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Requests
{
    public class WorkoutDataRequests
    {
        public int? Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int? ExercisId { get; set; }
        [Range(1,10)]
        [Required]
        public int SetNumber { get; set; }
        [Required]
        public int RepsNumber { get; set; }
        [Required]
        public double Weight { get; set; }
    }
}
