using SubscriptionApi.Abstraction;
using SubscriptionApi.Data;
using SubscriptionApi.Models;
using System;
using System.Linq;

public class TokenRepository: ITokenRepository
{
    private readonly AppDbContext _context;

    public TokenRepository(AppDbContext context)
    {
        _context = context;
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
}
