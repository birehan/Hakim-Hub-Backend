using MediatR;
using Application.Responses;
using Domain;
using Application.Features.Accounts.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Services;

namespace Application.Features.Accounts.CQRS.Queries

{
    public class LoginQuery : IRequest<Result<UserDto>>
    {
        public LoginDto LoginDto { get; set; }
    }
}

    