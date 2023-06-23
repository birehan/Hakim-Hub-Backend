using MediatR;
using Application.Responses;
using Application.Features.Accounts.DTOs;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public CreateUserDto CreateUserDto { get; set; }
    }

}