using MediatR;
using Application.Responses;
using Application.Features.Accounts.DTOs;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class ForgetPasswordSendEmailCommand : IRequest<Result<string>>
    {
        public ForgetPasswordDto ForgetPasswordDto { get; set; }
    }
}