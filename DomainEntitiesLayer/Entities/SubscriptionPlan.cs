using System.ComponentModel.DataAnnotations;


namespace DomainEntitiesLayer.Entities
{
    public class SubscriptionPlan : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }
        public int Period { get; set; }
        public string TimeUnit { get; set; }
        public float price { get; set; }
        public bool? IsActive { get; set; }


    }
}
