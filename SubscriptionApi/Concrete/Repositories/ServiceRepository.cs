using SubscriptionApi.Abstraction;
using SubscriptionApi.Data;
using SubscriptionApi.Models;
using System;
using System.Linq;

public class ServiceRepository: IServiceRepository
{
    private readonly AppDbContext _context;

    public ServiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public Service GetServiceByServiceId(string serviceId)
    {
        return _context.Services.SingleOrDefault(s => s.ServiceId == serviceId);
    }
    public Service GetServiceByServiceName(string serviceName)
    {
        return _context.Services.SingleOrDefault(s => s.ServiceName == serviceName);
    }
    public void UpdateService(Service service)
    {
        _context.Services.Update(service);
        _context.SaveChanges();
    }
    public Token GetValidToken(string serviceId)
    {
        var token = new Token();
        token = _context.Tokens
            .Where(t => t.ServiceId == serviceId && t.ExpiresOn > DateTime.UtcNow)
            .OrderByDescending(t => t.ExpiresOn)
            .FirstOrDefault();
        if (token == null)
        {
            return null;
        }
        return token;
    }
    public void AddToken(Token token)
    {
        _context.Tokens.Add(token);
        _context.SaveChanges();
    }
    public string AddService(Service service)
    {
        _context.Services.Add(service);
        _context.SaveChanges();
        return service.ServiceId;

    }
}
