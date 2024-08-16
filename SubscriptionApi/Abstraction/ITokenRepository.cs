using SubscriptionApi.Models;

namespace SubscriptionApi.Abstraction
{
    public interface ITokenRepository
    {
        void AddToken(Token token);
        Token GetValidToken(string serviceId);
    }
}
