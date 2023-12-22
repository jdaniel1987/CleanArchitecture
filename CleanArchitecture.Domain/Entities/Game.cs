using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public record Game(
    int Id,
    string Name,
    string Publisher,
    Price Price);
