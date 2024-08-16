namespace SubscriptionApi.Dtos.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
