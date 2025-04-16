using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class WorkoutDataResponses
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string ExerciseName { get; set; }
        public int SetNumber { get; set; }
        public int RepsNumber { get; set; }
        public float Weight { get; set; }
    }
}
