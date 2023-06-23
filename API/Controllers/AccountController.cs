using System.Security.Claims;
using Application.Features.Accounts.DTOs;
using  Application.Services;
using Application.Responses;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.Features.Accounts.CQRS.Commands;
using Application.Features.Accounts.CQRS.Queries;
using MediatR;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return HandleResult(await _mediator.Send(new LoginQuery { LoginDto = loginDto }));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(CreateUserDto CreateUserDto)
        {
            return HandleResult(await _mediator.Send(new CreateUserCommand { CreateUserDto = CreateUserDto }));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetLogedinUser()
        {
            return HandleResult(await _mediator.Send(new GetCurrentUserQuery()));
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("users")]
        public async Task<ActionResult<UserDto>> GetUsers()
        {
            return HandleResult(await _mediator.Send(new GetUsersListQuery()));
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpDelete("{username}")]
        public async Task<ActionResult<UserDto>> DeleteUser(string username)
        {
            return HandleResult(await _mediator.Send(new DeleteUserCommand { UserName = username }));
        }

        [Authorize]
        [HttpPut("updatePassword")]
        public async Task<ActionResult<UserDto>> UpdatePassword(UpdatePasswordDto updateUserPasswordDto)
        {
            return HandleResult(await _mediator.Send(new UpdatePasswordCommand { UpdatePasswordDto = updateUserPasswordDto }));
        }


        [Authorize(Policy = "IsAdmin")]
        [HttpPut("updateRole")]
        public async Task<ActionResult<UserDto>> UpdateRole(UpdateUserRoleDto updateUserRoleDto)
        {
            return HandleResult(await _mediator.Send(new UpdateUserRoleCommand { UpdateUserRoleDto = updateUserRoleDto }));
        }

        [AllowAnonymous]
        [HttpPut("forgetPasswordSendEmail")]
        public async Task<ActionResult<string>> ForgetPasswordSendEmail(ForgetPasswordSendEmailCommand command)
        {
            return HandleResult(await _mediator.Send(command));
        }


        [AllowAnonymous]
        [HttpPut("resetPassword")]
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordDto resetUserPasswordDto)
        {
            return HandleResult(await _mediator.Send(new ResetPasswordCommand { ResetPasswordDto = resetUserPasswordDto }));
        }

    }
}