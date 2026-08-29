using BookStore.Core.Enums;

namespace BookStore.Core.Constants;

public class AuthorizationOptions
{
    public RolePermissions[] RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public Role Role { get; set; } = Role.User;

    public Permissions[] Permission { get; set; } = [];
}
