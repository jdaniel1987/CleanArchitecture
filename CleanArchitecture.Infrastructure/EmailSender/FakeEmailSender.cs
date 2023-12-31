﻿using Microsoft.Extensions.Logging;
using CleanArchitecture.Domain.EmailSender;

namespace CleanArchitecture.Infrastructure.EmailSender;

public class FakeEmailSender : IEmailSender
{
    private readonly ILogger<FakeEmailSender> _logger;

    public FakeEmailSender(ILogger<FakeEmailSender> logger)
    {
        _logger = logger;
    }

    public async Task SendNotification(string email, string subject, string body)
    {
        _logger.LogInformation("Sent mail to {Email} ", email);
        _logger.LogInformation("Subject: {Subject}", subject);
        _logger.LogInformation("Body: {Body}", body);

        await Task.Delay(1000);
    }
}
