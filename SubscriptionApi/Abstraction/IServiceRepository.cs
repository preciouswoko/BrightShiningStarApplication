using SubscriptionApi.Models;

namespace SubscriptionApi.Abstraction
{
    public interface IServiceRepository
    {
        string AddService(Service service);
        void AddToken(Token token);
        Service GetServiceByServiceId(string serviceId);
        Service GetServiceByServiceName(string serviceName);
        Token GetValidToken(string serviceId);
        void UpdateService(Service service);
    }
}
