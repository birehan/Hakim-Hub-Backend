using MediatR;
using Application.Responses;
using Application.Features.Accounts.DTOs;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class UpdatePasswordCommand : IRequest<Result<string>>
    {
        public UpdatePasswordDto UpdatePasswordDto { get; set; }
    }
}