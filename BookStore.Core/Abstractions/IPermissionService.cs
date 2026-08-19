using System;
using BookStore.Core.Enums;

namespace BookStore.Core;

public interface IPermissionsService
{
    Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId);
}
