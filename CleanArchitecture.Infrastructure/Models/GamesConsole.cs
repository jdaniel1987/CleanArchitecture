using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Infrastructure.Models;

public class GamesConsole
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public double Price { get; set; }

    public virtual IReadOnlyCollection<Game> Games { get; set; } = new List<Game>();
}
