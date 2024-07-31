using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Services.Events;

public record GameCreatedEvent(
    Game Game,
    DateTime CreationDate) : INotification;
