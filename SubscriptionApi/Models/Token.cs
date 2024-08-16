using System.ComponentModel.DataAnnotations;

namespace SubscriptionApi.Models
{
    public class Token
    {
        [Key]
        public Guid TokenId { get; set; }
        public string ServiceId { get; set; }
        public string TokenValue { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
