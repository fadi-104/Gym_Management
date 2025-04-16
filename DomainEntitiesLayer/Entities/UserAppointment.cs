using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Entities
{
    public class UserAppointment : BaseEntity
    {
        public int UserId { get; set; }
        public int appoId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(appoId))]
        public TrainerAppointment TrainerAppointment { get; set; }
    }
}
