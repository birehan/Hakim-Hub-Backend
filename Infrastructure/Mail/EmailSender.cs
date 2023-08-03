using Application.Contracts.Infrastructure;
using Application.Models;
using Application.Responses;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    private MimeMessage CreateEmailMessage(Email email)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailSettings.From));
        emailMessage.To.Add(new MailboxAddress("email", email.To));
        emailMessage.Subject = email.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email.Body };
        return emailMessage;
    }

    public async Task<Result<Email>> sendEmail(Email email)
    {
        var result = new Result<Email>();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            var sent = await client.SendAsync(CreateEmailMessage(email));
            result.IsSuccess = true;
        }
        catch(Exception ex)
        {
            result.IsSuccess = false;
            result.Error = ex.Message; 
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
        

        if(result.IsSuccess)
            result.Value = email;

        return result;
    }
}

