using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class TrainerAppoResponses
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string TrainerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
