using MediatR;
using Application.Responses;
using Domain;
using Application.Features.Accounts.DTOs;
using Microsoft.AspNetCore.Identity;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Application.Features.Accounts.CQRS.Queries

{
    public class GetCurrentUserQuery : IRequest<Result<UserDto>>
    {
    }
}
    