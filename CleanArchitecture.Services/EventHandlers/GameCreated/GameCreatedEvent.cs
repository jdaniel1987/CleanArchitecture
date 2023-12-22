using MediatR;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Services.EventHandlers.GameCreated;

public record GameCreatedEvent(
    Game Game,
    DateTime CreationDate) : INotification;
