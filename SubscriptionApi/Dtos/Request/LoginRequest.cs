namespace SubscriptionApi.Dtos.Request
{
    public class LoginRequest
    {
        public Guid ServiceId { get; set; }
        public string Password { get; set; }
    }
}
