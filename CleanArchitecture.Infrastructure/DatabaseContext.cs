using CleanArchitecture.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    public virtual DbSet<GamesConsole> GamesConsoles { get; set; }
    public virtual DbSet<Game> Games { get; set; }
}
