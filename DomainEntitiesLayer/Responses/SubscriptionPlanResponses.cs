using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainEntitiesLayer.Responses
{
    public class SubscriptionPlanResponses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Period { get; set; }
        public float Price { get; set; }
        public bool IsActive { get; set; }
    }
}
