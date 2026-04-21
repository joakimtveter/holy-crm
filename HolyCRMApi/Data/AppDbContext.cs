using HolyCRMApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyCRMApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Event> Events => Set<Event>();

}