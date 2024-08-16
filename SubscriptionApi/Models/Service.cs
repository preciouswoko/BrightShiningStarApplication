using System.ComponentModel.DataAnnotations;

namespace SubscriptionApi.Models
{
    public class Service
    {
        [Key]
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string PasswordHash { get; set; }
        public int TokenExpirationHours { get; set; }
    }
}
