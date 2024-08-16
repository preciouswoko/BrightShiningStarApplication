using System.ComponentModel.DataAnnotations;

namespace SubscriptionApi.Models
{
    public class Subscriber
    {
        [Key]
        public string SubscriptionId { get; set; }
        public string ServiceId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime SubscribedOn { get; set; }
        public DateTime? UnsubscribedOn { get; set; }
        public bool IsSubscribed { get; set; }

    }
}
