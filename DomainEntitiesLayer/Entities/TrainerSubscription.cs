using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DomainEntitiesLayer.Entities
{
    public class TrainerSubscription : BaseEntity
    {
        public int TraineeId { get; set; }
        public int TrainerId { get; set; }

        [ForeignKey(nameof(TraineeId))]
        public AppUser Trainee { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public AppUser Trainer { get; set; }
    }
}
