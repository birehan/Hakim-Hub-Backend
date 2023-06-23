using MediatR;
using Application.Responses;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Features.Accounts.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class UpdateUserRoleCommand : IRequest<Result<string>>
    {
        public UpdateUserRoleDto UpdateUserRoleDto { get; set; }
    }
}
    