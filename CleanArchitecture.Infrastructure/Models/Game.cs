using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Infrastructure.Models;

public class Game
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Publisher { get; set; }
    public required int GamesConsoleId { get; set; }
    public required double Price { get; set; }

    public virtual GamesConsole GamesConsole { get; set; } = null!;
}
