using BookStore.Core;
using BookStore.Core.Abstractions;
using BookStore.Core.Enums;

namespace BookStore.Application;

public class PermissionsService(IUserRepository userRepository) : IPermissionsService
{
    private readonly IUserRepository _userRepository = userRepository;

    public Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId)
    {
        return _userRepository.GetUserPermissions(userId);
    }
}
