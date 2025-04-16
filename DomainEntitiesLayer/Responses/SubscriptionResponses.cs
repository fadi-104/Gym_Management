using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntitiesLayer.Responses
{
    public class SubscriptionResponses
    {
        public int Id { get; set; }
        public string UserName{ get; set; }
        public string SubscriptionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float price { get; set; }
    }
}
