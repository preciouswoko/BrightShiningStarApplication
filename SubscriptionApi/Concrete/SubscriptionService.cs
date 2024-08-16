using SubscriptionApi.Abstraction;
using SubscriptionApi.Dtos.Request;
using SubscriptionApi.Dtos.Response;
using SubscriptionApi.Models;
using System;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriberRepository _subscriberRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IConfiguration _configuration;
    private string SecretKey;

    public SubscriptionService(ISubscriberRepository subscriberRepository, IServiceRepository serviceRepository, IConfiguration configuration)
    {
        _subscriberRepository = subscriberRepository;
        _serviceRepository = serviceRepository;
        _configuration = configuration;
        SecretKey = _configuration["AppSettings:SecretKey"];

    }

    public SubscriptionResult Subscribe(SubscribeRequest request)
    {
        var service = _serviceRepository.GetServiceByServiceId(request.ServiceId);
        if (service == null || !TokenUtility.ValidateToken(request.Token, SecretKey).Identity.Name.Equals(service.ServiceId))
        {
            return null;
        }

        var existingSubscriber = _subscriberRepository.GetSubscriber(request.ServiceId, request.PhoneNumber);
        if (existingSubscriber != null && existingSubscriber.UnsubscribedOn == null)
        {
            return new SubscriptionResult
            {
                SubscriptionId = existingSubscriber.SubscriptionId,
                Message = "User is already subscribed"
            };
        }

        var subscriber = new Subscriber
        {
            ServiceId = request.ServiceId.ToString(),
            PhoneNumber = request.PhoneNumber,
            SubscribedOn = DateTime.UtcNow,
            IsSubscribed = true,
            SubscriptionId = Guid.NewGuid().ToString()
        };

      string SubscriptionId = _subscriberRepository.AddSubscriber(subscriber);

        return new SubscriptionResult
        {
            SubscriptionId = SubscriptionId,
            Message = "User subscribed successfully"
        };
    }

    public bool Unsubscribe(UnsubscribeRequest request)
    {
        var service = _serviceRepository.GetServiceByServiceId(request.ServiceId.ToString());
        if (service == null || !TokenUtility.ValidateToken(request.Token, SecretKey).Identity.Name.Equals(service.ServiceId))
        {
            return false;
        }

        var subscriber = _subscriberRepository.GetSubscriber(request.ServiceId.ToString(), request.PhoneNumber);
        if (subscriber == null || subscriber.UnsubscribedOn != null)
        {
            return false;
        }

        subscriber.UnsubscribedOn = DateTime.UtcNow;
        _subscriberRepository.UpdateSubscriber(subscriber);

        return true;
    }

    public SubscriberStatus CheckStatus(CheckStatusRequest request)
    {
        var service = _serviceRepository.GetServiceByServiceId(request.ServiceId.ToString());
        if (service == null || !TokenUtility.ValidateToken(request.Token, SecretKey).Identity.Name.Equals(service.ServiceId))
        {
            return null;
        }

        var subscriber = _subscriberRepository.GetSubscriber(request.ServiceId.ToString(), request.PhoneNumber);
        if (subscriber == null)
        {
            return null;
        }

        return new SubscriberStatus
        {
            IsSubscribed = subscriber.UnsubscribedOn == null,
            SubscribedOn = subscriber.SubscribedOn,
            UnsubscribedOn = subscriber.UnsubscribedOn
        };
    }
}
