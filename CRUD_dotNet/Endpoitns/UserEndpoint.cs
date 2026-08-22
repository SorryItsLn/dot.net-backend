using BookStore.API.Contracts.Users;
using BookStore.Core.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookStore.API.Endpoints.Users
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(
            RegisterUserRequest request,
            IUserService userService
        )
        {
            var result = await userService.Register(
                request.UserName,
                request.Email,
                request.Password
            );

            return !result.IsSuccess
                ? Results.BadRequest(new { error = result.Error })
                : Results.Ok();
        }

        private static async Task<IResult> Login(
            LoginUserRequest request,
            IUserService userService,
            HttpContext context
        )
        {
            var token = await userService.Login(request.Email, request.Password);
            context.Response.Cookies.Append("access", token);

            return Results.Ok(token);
        }
    }
}
