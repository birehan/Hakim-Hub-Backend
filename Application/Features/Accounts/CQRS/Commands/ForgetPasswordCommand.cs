using MediatR;
using Application.Responses;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Infrastructure;
using Application.Models;
using Application.Features.Accounts.DTOs;

namespace Application.Features.Accounts.CQRS.Commands
{
    public class ForgetPasswordSendEmailCommand : IRequest<Result<string>>
    {
        public ForgetPasswordDto ForgetPasswordDto { get; set; }
    }
}