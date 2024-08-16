namespace SubscriptionApi.Dtos.Request
{
    public class CheckStatusRequest
    {
        public Guid ServiceId { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
}
