using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Infrastructure.Models;

public class GamesConsole
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Manufacturer { get; set; }
    public required double Price { get; set; }

    public virtual IReadOnlyCollection<Game> Games { get; set; } = new List<Game>();
}
