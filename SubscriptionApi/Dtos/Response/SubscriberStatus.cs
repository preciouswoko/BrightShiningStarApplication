namespace SubscriptionApi.Dtos.Response
{
    public class SubscriberStatus
    {
        public bool IsSubscribed { get; set; }
        public DateTime SubscribedOn { get; set; }
        public DateTime? UnsubscribedOn { get; set; }
    }

}
