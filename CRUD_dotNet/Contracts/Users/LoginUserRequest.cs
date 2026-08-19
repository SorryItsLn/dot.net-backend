using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Contracts.Users
{
    public record LoginUserRequest([Required] string Email, [Required] string Password);
}