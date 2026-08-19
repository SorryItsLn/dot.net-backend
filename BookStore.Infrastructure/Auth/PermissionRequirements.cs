using System;
using BookStore.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Infrastructure.Auth;

public class PermissionRequirements(Permissions[] permissions) : IAuthorizationRequirement
{
    public Permissions[] Permissions { get; set; } = permissions;
}
