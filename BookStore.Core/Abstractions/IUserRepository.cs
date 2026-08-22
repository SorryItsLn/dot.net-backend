using BookStore.Core.Enums;
using BookStore.Core.Helpers;
using BookStore.Core.Models;

namespace BookStore.Core.Abstractions
{
    public interface IUserRepository
    {
        Task<Result<User>> AddUser(User user);

        Task<User> GetByEmail(string email);

        Task<HashSet<Permissions>> GetUserPermissions(Guid userId);
    }
}
