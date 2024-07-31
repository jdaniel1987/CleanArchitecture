using CleanArchitecture.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public virtual DbSet<GamesConsole> GamesConsoles { get; set; }
    public virtual DbSet<Game> Games { get; set; }
}
