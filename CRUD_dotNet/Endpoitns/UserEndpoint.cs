using BookStore.API.Contracts.Users;
using BookStore.Core.Abstractions;

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
            await userService.Register(request.UserName, request.Email, request.Password);
            return Results.Ok();
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