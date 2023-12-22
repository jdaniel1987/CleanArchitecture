﻿namespace CleanArchitecture.Domain.EmailSender;

public interface IEmailSender
{
    Task SendNotification(string email, string subject, string body);
}
