using Microsoft.EntityFrameworkCore;
using SubscriptionApi.Models;

namespace SubscriptionApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        
    }
}
