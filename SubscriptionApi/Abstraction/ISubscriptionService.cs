using SubscriptionApi.Dtos.Request;
using SubscriptionApi.Dtos.Response;

namespace SubscriptionApi.Abstraction
{
    public interface ISubscriptionService
    {
        SubscriberStatus CheckStatus(CheckStatusRequest request);
        SubscriptionResult Subscribe(SubscribeRequest request);
        bool Unsubscribe(UnsubscribeRequest request);
    }
}
