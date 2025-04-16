using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class TrainerSubscriptionResponses
    {
        public int Id { get; set; }
        public string TraineeFullName { get; set; }
        public string TrainerFullName { get; set; }
    }
}
