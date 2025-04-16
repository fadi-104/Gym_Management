using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Entities
{
    public class Subscription : BaseEntity
    {
        public int UserId { get; set; }
        public int? SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(SubscriptionId))]
        public SubscriptionPlan Plan { get; set; }
    }
}
