using System;
using BookStore.API.Contracts.Users;
using BookStore.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("user")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] RegisterUserRequest request)
    {
        var result = await _userService.Register(request.UserName, request.Email, request.Password);

        return !result.IsSuccess ? Results.BadRequest(new { error = result.Error }) : Results.Ok();
    }

    [HttpPost("login")]
    public async Task<IResult> Login([FromBody] LoginUserRequest request)
    {
        var token = await _userService.Login(request.Email, request.Password);
        HttpContext.Response.Cookies.Append("access", token);

        return Results.Ok(token);
    }
}
