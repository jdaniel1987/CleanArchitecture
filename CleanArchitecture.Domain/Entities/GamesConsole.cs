using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public record GamesConsole(
    int Id,
    string Name,
    string Manufacturer,
    Price Price);
