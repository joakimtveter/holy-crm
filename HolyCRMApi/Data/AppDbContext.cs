using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace HolyCRMApi.Data;

/// <summary>
/// The application's primary database context.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>Gets the members table.</summary>
    public DbSet<Member> Members => Set<Member>();

    /// <summary>Gets the events table.</summary>
    public DbSet<Event> Events => Set<Event>();

    /// <summary>Gets the venues table.</summary>
    public DbSet<Venue> Venues => Set<Venue>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().Property(m => m.Id).HasValueGenerator<GuidV7Generator>();
        modelBuilder.Entity<Event>().Property(e => e.Id).HasValueGenerator<GuidV7Generator>();
        modelBuilder.Entity<Venue>().Property(v => v.Id).HasValueGenerator<GuidV7Generator>();
    }
}

/// <summary>
/// Generates time-ordered, RFC 4122 compliant UUID v7 values for entity primary keys.
/// </summary>
file sealed class GuidV7Generator : ValueGenerator<Guid>
{
    /// <inheritdoc/>
    public override Guid Next(EntityEntry entry) => Guid.CreateVersion7();

    /// <inheritdoc/>
    public override bool GeneratesTemporaryValues => false;
}
