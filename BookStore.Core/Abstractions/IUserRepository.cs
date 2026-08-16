using BookStore.Core.Models;

namespace BookStore.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);

        Task<User> GetByEmail(string email);
    }
}
