namespace SubscriptionApi.Dtos.Request
{
    public class SubscribeRequest
    {
        public string ServiceId { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
}
