using Microsoft.EntityFrameworkCore;
using LeadFlow.Api.Models;

namespace LeadFlow.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base( options )
    {
    }

    public DbSet<Campaign> Campaigns => Set<Campaign>();
}