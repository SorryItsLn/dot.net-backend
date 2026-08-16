namespace BookStore.API.Contracts.Users
{
    public record RegisterUserRequest(string UserName, string Password, string Email);
}
