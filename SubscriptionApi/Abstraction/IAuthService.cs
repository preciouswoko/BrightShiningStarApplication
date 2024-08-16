using SubscriptionApi.Dtos.Request;
using SubscriptionApi.Dtos.Response;

namespace SubscriptionApi.Abstraction
{
    public interface IAuthService
    {
        string CreateService(CreateServiceRequest request);
        LoginResponse Login(LoginRequest request);
    }
}
