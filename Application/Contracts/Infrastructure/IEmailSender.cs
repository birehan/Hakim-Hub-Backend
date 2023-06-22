using System.Collections.Generic;
using System.Text;
using Application.Models;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}