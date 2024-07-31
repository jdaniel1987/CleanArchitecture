using AutoFixture.Xunit2;
using CleanArchitecture.Domain.EmailSender;
using CleanArchitecture.Domain.ValueObjects;
using CleanArchitecture.Services.Events;
using Moq;

namespace CleanArchitecture.Services.EventHandlers.GameCreated;

public class GameCreatedEventHandlerTests
{
    [Theory, AutoData]
    public async Task Handle_should_send_notification(
        Mock<IEmailSender> emailSenderMock,
        GameCreatedEvent gameCreatedEvent)
    {
        var handler = new GameCreatedEventHandler(
            emailSenderMock.Object
        );

        await handler.Handle(gameCreatedEvent, CancellationToken.None);

        emailSenderMock.Verify(s => s.SendNotification(
            "random@email.com",
            $"{gameCreatedEvent.CreationDate.ToString("dd-MM-yyyy HH:mm")} - New Game from {gameCreatedEvent.Game.Publisher}",
            $"Product {gameCreatedEvent.Game.Name} with price USD {gameCreatedEvent.Game.Price} / EUR {(PriceEuros)gameCreatedEvent.Game.Price}"
            ), Times.Once);
    }
}
