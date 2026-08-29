using BookStore.Core.Enums;

namespace BookStore.Core.Constants;

public class RolePermissionsRelationship
{
    private static readonly IReadOnlyDictionary<Role, Permissions[]> RolesRelationship =
        new Dictionary<Role, Permissions[]>
        {
            [Role.Admin] =
            [
                Permissions.Read,
                Permissions.Create,
                Permissions.Update,
                Permissions.Delete,
            ],
            [Role.User] = [Permissions.Read],
            [Role.Creator] = [Permissions.CreateEvent],
        };

    public static AuthorizationOptions GetRoleRelationship()
    {
        var rolePermissions = RolesRelationship
            .Select(kvp => new RolePermissions { Role = kvp.Key, Permission = kvp.Value })
            .ToArray();

        return new AuthorizationOptions { RolePermissions = rolePermissions };
    }
}
