using Microsoft.EntityFrameworkCore;
using TicketCentral.Infrastructure.Models;

namespace TicketCentral.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<OrganiserProfile> OrganiserProfiles { get; set; }
    public DbSet<Event> Events { get; set; }

}