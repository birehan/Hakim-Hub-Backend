using Application.Models;
using Application.Responses;

namespace Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<Result<Email>> sendEmail(Email email);
}
