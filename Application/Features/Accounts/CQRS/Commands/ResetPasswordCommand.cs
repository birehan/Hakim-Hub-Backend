using Application.Features.Accounts.DTOs;
using Application.Responses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class ResetPasswordCommand : IRequest<Result<string>>
    {
        public ResetPasswordDto ResetPasswordDto { get; set; }
    }
}
