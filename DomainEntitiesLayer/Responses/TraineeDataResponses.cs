using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class TraineeDataResponses
    {
        public int Id { get; set; }
        public string TraineeName { get; set; }
        public float Weight { get; set; }
        public int Height { get; set; }
        public float WeightGoal { get; set; }
        public string Goal { get; set; }
    }
}
