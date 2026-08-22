using BookStore.Core.Helpers;
using BookStore.Core.Models;

namespace BookStore.Core.Abstractions;

public interface IUserService
{
    Task<string> Login(string email, string password);
    Task<Result<User>> Register(string userName, string email, string password);
}
