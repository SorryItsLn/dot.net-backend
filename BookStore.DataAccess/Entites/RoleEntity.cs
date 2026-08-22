using BookStore.Core.Models;
using BookStore.DataAccess.Entities;

namespace BookStore.DataAccess;

public class RoleEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<PermissionsEntity> Permissions { get; set; } = [];
    public ICollection<UserEntity> Users { get; set; } = [];
}
