using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Requests
{
    public class SubscriptionPlanRequests
    {
        public int? Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        public int Period { get; set; }
        [Required]
        [RegularExpression("^(Month|Year)$", ErrorMessage = "The value must be either 'Month' or 'Year'.")]
        public string TimeUnit { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
