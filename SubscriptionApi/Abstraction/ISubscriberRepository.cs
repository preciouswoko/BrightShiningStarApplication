using SubscriptionApi.Models;

namespace SubscriptionApi.Abstraction
{
    public interface ISubscriberRepository
    {
        string AddSubscriber(Subscriber subscriber);
        Subscriber GetSubscriber(string serviceId, string phoneNumber);
        void UpdateSubscriber(Subscriber subscriber);
    }
}
