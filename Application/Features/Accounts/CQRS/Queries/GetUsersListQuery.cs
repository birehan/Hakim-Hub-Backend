
using MediatR;
using Application.Responses;
using Application.Features.Accounts.DTOs;

namespace Application.Features.Accounts.CQRS.Queries

{
    public class GetUsersListQuery : IRequest<Result<List<UserAccountDto>>>
    {
    }

}