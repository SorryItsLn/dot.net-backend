using System;

namespace BookStore.DataAccess;

public class UserRoleEntity
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }
}
