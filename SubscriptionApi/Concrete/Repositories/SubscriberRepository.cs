using SubscriptionApi.Abstraction;
using SubscriptionApi.Data;
using SubscriptionApi.Models;
using System;
using System.Linq;

public class SubscriberRepository : ISubscriberRepository
{
    private readonly AppDbContext _context;

    public SubscriberRepository(AppDbContext context)
    {
        _context = context;
    }

    public Subscriber GetSubscriber(string serviceId, string phoneNumber)
    {
        return _context.Subscribers.SingleOrDefault(s => s.ServiceId == serviceId && s.PhoneNumber == phoneNumber);
    }

    public string AddSubscriber(Subscriber subscriber)
    {
        _context.Subscribers.Add(subscriber);
        _context.SaveChanges();
        return subscriber.SubscriptionId; 
    }

    public void UpdateSubscriber(Subscriber subscriber)
    {
        _context.Subscribers.Update(subscriber);
        _context.SaveChanges();
    }
}
