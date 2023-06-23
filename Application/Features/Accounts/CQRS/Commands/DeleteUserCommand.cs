using MediatR;
using Application.Responses;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class DeleteUserCommand : IRequest<Result<string>>
    {
        public string UserName { get; set; }
    }
}

    