using BookStore.Core.Enums;
using BookStore.Core.Helpers;
using BookStore.Core.Models;

namespace BookStore.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Result<User>> AddUser(User user);

    Task<User> GetById(Guid id);

    Task<User> GetByEmail(string email);

    Task<HashSet<Permissions>> GetUserPermissions(Guid userId);
}
