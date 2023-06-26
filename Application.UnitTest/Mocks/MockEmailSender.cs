using Application.Contracts.Identity;
using Application.Models.Mail;
using Infrastructure.Mail;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.Tests.Mocks
{
    public static class MockEmail
    {
        public static IEmailSender CreateEmailSender()
        {
            var emailSettings = new EmailSettings
            {
                ApiKey = "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",
                FromAddress = "sosna@a2sv.org",
                FromName = "sosna@a2sv.org"
            };

            var optionsMock = new Mock<IOptions<EmailSettings>>();
            optionsMock.Setup(x => x.Value).Returns(emailSettings);

            return new EmailSender(optionsMock.Object);
        }
    }
}