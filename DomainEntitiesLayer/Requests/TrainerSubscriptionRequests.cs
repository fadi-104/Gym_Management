using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Requests
{
    public class TrainerSubscriptionRequests
    {
        public int? Id { get; set; }
        [Required]
        public int TraineeId { get; set; }
        [Required]
        public int TrainerId { get; set; }
    }
}
