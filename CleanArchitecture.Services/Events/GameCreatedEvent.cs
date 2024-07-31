using MediatR;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.Events;

public record GameCreatedEvent(
    Game Game,
    DateTime CreationDate) : INotification;
