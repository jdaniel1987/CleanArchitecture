using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Infrastructure.Models;

public class Game
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public int GamesConsoleId { get; set; }
    public double Price { get; set; }

    public virtual GamesConsole GamesConsole { get; set; } = null!;
}
