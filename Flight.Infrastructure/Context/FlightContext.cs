using Flight.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flight.Infrastructure.Context;

public class FlightContext(DbContextOptions<FlightContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<RoutesEntity>().HasKey(m => m.Id);
        base.OnModelCreating(builder);
    }
    public DbSet<RoutesEntity> Routes { get; set; } = null!;
}